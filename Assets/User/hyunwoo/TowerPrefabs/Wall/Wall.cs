using Lean.Pool;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private GridXY grid;
    public GameObject wallBridge;
    private Vector3 boxsize = new Vector3(3, 3, 3);

    private Vector2 thisCellPos;
    private Vector3 additionalPos;

    private void Awake() {
        grid = MapManager.Instance.grid;
        additionalPos = new Vector3(5, 0, 5);
    }
    private void OnEnable() {
        grid = MapManager.Instance.grid;
        grid.GetXY(transform.position, out int Posx, out int Posy);
        thisCellPos = new Vector2(Posx, Posy);

        CheckWall();
    }
    
    private void CheckWall() {
        int[,] checkarr = CheckList((int)thisCellPos.x, (int)thisCellPos.y);

        for(int i = 0; i < checkarr.GetLength(0); i++) {
            Vector3 cellPos = grid.GetWorldPosition(checkarr[i, 0], checkarr[i, 1]) + additionalPos;
            Collider[] cols = Physics.OverlapBox(cellPos, boxsize, Quaternion.identity, 1 << 7 | 1 << 31);
            foreach (var item in cols)
            {
                if(item.gameObject.tag == "Wall") {
                    CreateWallBridge(cellPos, item.GetComponentInParent<Wall>(), false);
                    break;
                }

                if(item.gameObject.tag == "Mount") {
                    CreateWallBridge(cellPos, item.GetComponentInParent<Wall>(), true);
                    break;
                }
            }
        }
        
    }

    private int[,] CheckList(int x, int y) {
        return new int[,]{{x + 1, y}, {x - 1, y}, {x, y - 1}, {x, y + 1}} ;
    }

    private void CreateWallBridge(Vector3 cellPos, Wall targetWall, bool isMount) {
        Vector3 thisPos = transform.position + additionalPos;
        Vector3 direction = (thisPos - cellPos) * -1;
        Vector3 createPos = thisPos + (direction / 2);

        Quaternion rotate;
        if( direction.z != 0 ) rotate = Quaternion.Euler(new Vector3(0, 90, 0));
        else rotate = Quaternion.identity; 

        GameObject bridge = LeanPool.Spawn(wallBridge, createPos, rotate);
        WallBridge bridgeComponent = bridge.GetComponent<WallBridge>();
        bridgeComponent.mainBodyWall = this;
        bridgeComponent.targetWall = targetWall;

        bridgeComponent.isMount = isMount;
        
    }
}
