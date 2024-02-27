using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Lean.Pool;


public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private List<MonsterData>       monsterDatas;
    [SerializeField]
    private GameObject              monsterSpawnPoint;
    [SerializeField]
    private Monster                 currentMonsterPrefab;

    private int                     spawnData;
    private int                     spawnDataRandomValue;
    private int                     spawnPosX;
    private int                     spawnPosY;

    private Transform               spawnTransform;
    // private 
    private void Awake()
    {
        spawnData = monsterDatas.Count;

    }
    public void SpawnMonster()
    {
        spawnDataRandomValue = Random.Range(0, spawnData);
        spawnPosX = Random.Range(-8, 9);
        spawnPosY = Random.Range(-8, 9);
        Vector3 randPos = new Vector3(monsterSpawnPoint.transform.position.x+ spawnPosX, monsterSpawnPoint.transform.position.y, monsterSpawnPoint.transform.position.z + spawnPosY);
        currentMonsterPrefab = LeanPool.Spawn(monsterDatas[spawnDataRandomValue].MonsterPrefab,
            randPos,Quaternion.identity).GetComponent<Monster>();
        currentMonsterPrefab.MonsterData = monsterDatas[spawnDataRandomValue];
        currentMonsterPrefab.state = Monster.State.chase;

    }
    public void SpawnTowerKeeper(MonsterTower _tower) 
    {
        spawnDataRandomValue = Random.Range(0, spawnData);
        spawnPosX = Random.Range(-8, 9);
        spawnPosY = Random.Range(-8, 9);

        Vector3 randPos = new Vector3(monsterSpawnPoint.transform.position.x + spawnPosX, 
            monsterSpawnPoint.transform.position.y, 
            monsterSpawnPoint.transform.position.z + spawnPosY);

        var currentKeeperPrefab = LeanPool.Spawn(monsterDatas[spawnDataRandomValue].MonsterPrefab,
            randPos, Quaternion.identity).GetComponent<Monster>();

        currentKeeperPrefab.MonsterData = monsterDatas[spawnDataRandomValue];
        currentKeeperPrefab.SetTowerObject(_tower);
        currentKeeperPrefab.state = Monster.State.towerReqair;
    }
}