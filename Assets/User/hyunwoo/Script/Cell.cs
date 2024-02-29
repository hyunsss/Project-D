using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int Posx;
    public int Posy;

    public bool isBuilding;

    private int Layermasks;

    private Vector3 boxsize = new Vector3(4, 6, 4);
    [HideInInspector] public MeshRenderer meshRenderer;

    public GameObject Target;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        Layermasks = 1 << 30 | 1 << 31;
    }

    private void Update() {
        MapManager.Instance.grid.GetXY(transform.position, out Posx, out Posy);

        if(Physics.OverlapBox(transform.position, boxsize, Quaternion.identity, Layermasks).Length != 0) {
            meshRenderer.material.color = Color.red;
        } else if(IsInOfArea() == false) {
            meshRenderer.material.color = Color.red;
        } else {
            meshRenderer.material.color = Color.green;
        } 

        if(BuildingManager.Instance.targetBuilding == null) {
            transform.position = new Vector3(999,999,999);
        }
    }

    private bool IsInOfArea() {
        GridXY grid = MapManager.Instance.grid; 
        return 0 <= Posx && Posx < grid.Width && 0 <= Posy && Posy < grid.Height;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, boxsize * 2);
        Gizmos.color = Color.yellow;
    }
        
}
