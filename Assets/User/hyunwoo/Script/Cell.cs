using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int Posx;
    public int Posy;

    public bool isBuilding;

    private Vector3 boxsize = new Vector3(4, 0.5f, 4);
    [HideInInspector] public MeshRenderer meshRenderer;

    public GameObject Target;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update() {
        MapManager.Instance.grid.GetXY(transform.position, out Posx, out Posy);

        Debug.Log(IsInOfArea());
        if(Physics.OverlapBox(transform.position, boxsize, Quaternion.identity, 1 << 30).Length != 0) {
            meshRenderer.material.color = Color.red;
        } else if(IsInOfArea() == false) {
            meshRenderer.material.color = Color.red;
        } else {
            meshRenderer.material.color = Color.green;
        } 
    }

    private bool IsInOfArea() {
        GridXY grid = MapManager.Instance.grid; 
        return 0 <= Posx && Posx < grid.Width && 0 <= Posy && Posy < grid.Height;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(8,1,8));
        Gizmos.color = Color.yellow;
    }
}
