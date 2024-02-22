using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public GameObject cell;

    [HideInInspector]public GridXY grid;
    public int currentMapWidth;
    public int currentMapHeight;
    public float cellsize;
    public Plane plane;

    private void Awake() {
        Instance = this;
        InitManager();
    }

    // Start is called before the first frame update
    void Start()
    {
        grid.InitGrid(currentMapWidth, currentMapHeight, cellsize);
        grid.GenerateGrid();
    }

    private void Update() {
        
    }

    public void InitManager() {
        grid = GetComponent<GridXY>();
        //y = 0인 평면 가상 플레인을 생성합니다.
        plane = new Plane(Vector3.up, Vector3.zero);
    }
}
