using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SpawnTowerPanel : MonoBehaviour
{
    public  SpawnTower TowerPrefab;
    private Button archerSpawnButton;
    private Button mageSpawnButton;
    private Button warriorSpawnButton;
    private Button workerSpawnButton;

    private void Start()
    {
        archerSpawnButton = transform.Find("ArcherButton").GetComponent<Button>();
        mageSpawnButton = transform.Find("MageButton").GetComponent<Button>();
        warriorSpawnButton = transform.Find("WarriorButton").GetComponent<Button>();
        workerSpawnButton = transform.Find("WorkerButton").GetComponent<Button>();

        archerSpawnButton.onClick.AddListener(() => TowerPrefab.SelectUnit(0));
        mageSpawnButton.onClick.AddListener(() => TowerPrefab.SelectUnit(1));
        warriorSpawnButton.onClick.AddListener(() => TowerPrefab.SelectUnit(2));
        workerSpawnButton.onClick.AddListener(() => TowerPrefab.SelectUnit(3));
    }
}
