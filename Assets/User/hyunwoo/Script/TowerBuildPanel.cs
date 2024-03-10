using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuildPanel : MonoBehaviour
{

    private Button CancelButton;
    private Button ArcherTowerButton;
    private Button CanonTowerButton;
    private Button SlowTowerButton;
    private Button SpawnUnitButton;
    private Button MineralTowerButton;
    private Button WallButton;

    private void Awake() {
        CancelButton = transform.Find("CancelButton").GetComponent<Button>();
        ArcherTowerButton = transform.Find("ArcherTowerButton").GetComponent<Button>();
        CanonTowerButton = transform.Find("CanonTowerButton").GetComponent<Button>();
        SlowTowerButton = transform.Find("SlowTowerButton").GetComponent<Button>();
        SpawnUnitButton = transform.Find("SpawnUnitTowerButton").GetComponent<Button>();
        MineralTowerButton = transform.Find("MineralTowerButton").GetComponent<Button>();
        WallButton = transform.Find("WallButton").GetComponent<Button>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        CancelButton.onClick.AddListener(() => BuildingManager.Instance.SetTarget(BuildingManager.BuildKey.None));
        ArcherTowerButton.onClick.AddListener(() => BuildingManager.Instance.SetTarget(BuildingManager.BuildKey.Archer));
        CanonTowerButton.onClick.AddListener(() => BuildingManager.Instance.SetTarget(BuildingManager.BuildKey.Canon));
        SlowTowerButton.onClick.AddListener(() => BuildingManager.Instance.SetTarget(BuildingManager.BuildKey.Slow));
        SpawnUnitButton.onClick.AddListener(() => BuildingManager.Instance.SetTarget(BuildingManager.BuildKey.SpawnUnit));
        MineralTowerButton.onClick.AddListener(() => BuildingManager.Instance.SetTarget(BuildingManager.BuildKey.Mineral));
        WallButton.onClick.AddListener(() => BuildingManager.Instance.SetTarget(BuildingManager.BuildKey.Wall));
        
    }
}
