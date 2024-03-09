using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Lean.Pool;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BuildingManager : MonoBehaviour
{
    public enum BuildKey { Archer, Canon, Slow, SpawnUnit, Mineral, Gas, Wall, None }
    public List<Installation> buildList = new List<Installation>();

    [HideInInspector]public Installation targetBuilding;

    public static BuildingManager Instance;
    private GridXY grid;
    private float enter;

    private bool isColliderClash;
    private bool isInstallPossible;

    private Vector3 InstallPosition;

    public void SetTarget(BuildKey key) {
        
        if(key == BuildKey.None) {
            targetBuilding = null; 
            return;
        }

        targetBuilding = buildList[(int)key];
    }

    private List<Cell> cells = new List<Cell>();
    Vector3 TempPos;

    // 그리드 전체에 cell을 생성할게 아니라 보이지 않는 그리드만 생성할 뿐, 
    // 플레이어가 건물 빌드를 하고자 할 때는 필요한 영역만 cell을 생성하여 마테리얼의 색을 바꾸며 알려주도록 한다. 
    // 필요한 구역에만 cell을 배치한다는 의미.
    // 포인터 엔터 핸들러는 빌딩매니저에서 하되, 건물을 지으려고 할때 타겟 빌딩 변수를 할당해주고 null이 아닐 경우만 핸들러가 작동하도록 한다. 
    // 맵이 구성되면 맵 전체의 콜라이더를 형성하여 거기다 이벤트 핸들러를 둔 다음 상호작용 시킨다?
    /*
        1. 플레이어의 스테이트가 건물을 지으려고 하는지 체크 targetbuilding -> not null일 때
        2. 가상 플레인이 레이를 발사하고 맞으면 그 위에 지을 수 있는지 없는지 여부를 아래의 함수에서 진행
        3. 먼저 가상플레인에 레이가 맞았을 경우 cell 오브젝트를 건물 크기에 맞게끔 생성하여 grid의 위치와 동일하게 한다.
        4. cell은 cell 스크립트 컴포넌트를 가지고 있고 update에서 해당 cell과 건물 콜라이더가 맞닿을 경우 mesh색을 빨간색으로 변경 아니면 초록으로 변경
        5. 주기적으로 cell들의 색상을 확인하여 지을 수 있는지 없는지 체크한다. 지을 수 있다면 마우스 입력을 허용하고 건물을 빌드 할 수 있도록한다.
        6. 예외 처리 : 건물과 맞닿았거나, 다른 콜라이더 ( 플레이어, 몬스터, 벽 )이 있어도 못짓게 끔 해야한다. 맵을 넘어갈 경우도 생각한다.
            -> 가상 플레인은 그리드를 지정한 구역외의 구역도 레이를 발사 하므로 그리드 구역 밖이라면 못짓도록 예외처리를 해주어야 한다.

    */

    private void Awake()
    {
        Instance = this;

        isInstallPossible = true;
        TempPos = Vector3.zero;
        targetBuilding = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        grid = MapManager.Instance.grid;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetBuilding != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //가상 평면에 레이와 마우스 클릭했을 때 쏜 레이가 맞았다면 그 아래 함수를 실행합니다.
            if (MapManager.Instance.plane.Raycast(ray, out enter))
            {
                //레이가 만난 지점에서 해당 포지션을 가져옵니다. 
                InstallPosition = ray.GetPoint(enter);
                grid.GetXY(InstallPosition, out int x, out int y);

                if (TempPos != grid.GetWorldPosition(x, y))
                {
                    CreateBuildArea(targetBuilding, x, y);
                    TempPos = grid.GetWorldPosition(x, y);
                }

            }

            if (Input.GetMouseButtonDown(0) && isInstallPossible == true && EventSystem.current.IsPointerOverGameObject() == false)
            {
                SearchBuilding(targetBuilding.gameObject, InstallPosition);
            }
            grid.GetXY(InstallPosition, out int posx, out int posy);
            // Debug.Log(grid.NodeFromWorldPoint(InstallPosition).worldPosition + "," + grid.GetWorldPosition(posx, posy));
            //Debug.Log(grid.NodeFromWorldPoint(InstallPosition).walkable);
        }

    }


    /// <summary>
    /// 플레이어에게 보여줄 건물의 영역 cell을 만들어 리스트에 담고 위치를 변경합니다.
    /// </summary>
    /// <param name="build"></param>
    /// <param name="posx"></param>
    /// <param name="posy"></param>
    private void CreateBuildArea(Installation build, int posx, int posy)
    {
        if (cells.Count != build.AreaWidth * build.AreaHeight)
        {
            foreach(Cell cell in cells) {
                LeanPool.Despawn(cell);
            }
            cells.Clear();
            
            for (int i = 0; i < build.AreaWidth * build.AreaHeight; i++)
            {
                Cell cell = LeanPool.Spawn(MapManager.Instance.cell, new Vector3(999, 999, 999), Quaternion.identity).GetComponent<Cell>();
                cells.Add(cell);
            }
        }


        //position을 바꾸는 반복문
        int index = 0;
        for (int X = posx; X < posx + build.AreaWidth; X++)
        {
            for (int Y = posy; Y < posy + build.AreaHeight; Y++)
            {
                // 셀의 위치를 업데이트
                Cell cell = cells[index];
                cell.transform.position = grid.GetWorldPosition(X, Y) + new Vector3(grid.CellSize / 2, 0, grid.CellSize / 2);

                index++;
            }
        }
    }

    /// <summary>
    /// 리스트에 담겨있는 cell들의 material 색들을 확인하고 설치 불가 영역 ( Color.red )인 영역이 하나라도 있다면 에러를 출력하고 리턴합니다.
    /// </summary>
    /// <param name="BuildPrefab"></param>
    /// <param name="position"></param>
    public void SearchBuilding(GameObject BuildPrefab, Vector3 position)
    {
        foreach (Cell cell in cells)
        {
            if (cell.meshRenderer.material.color == Color.red)
            {
                UI_PanelManager.Instance.DontBuildMessage();
                return;
            }
        }

        // 마테리얼 색이 모두 green일 경우 지을 수 있음

        //포지션을 얻고 건물 빌드하는 로직
        int x, y;
        grid.GetXY(position, out x, out y);
        GameObject Build = LeanPool.Spawn(BuildPrefab, new Vector3(x, 0, y) * grid.CellSize, Quaternion.identity);

        //지으려는 구역의 cell의 위치를 인자로 넘겨 해당 위치들의 node.walkable 값을 바꿈
        foreach (Cell cell in cells)
        {
            grid.ChangeWalkableNode(cell.transform.position, false);
        }

    }

    public void RemoveBuilding(Vector3 position)
    {
        int x, y;
        grid.GetXY(position, out x, out y);

        //Remove
    }

}
