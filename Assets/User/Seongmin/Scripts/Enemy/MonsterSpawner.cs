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

    }
    public void SpawnTowerKeeper(GameObject _repairTarget)  //TODO 타워 키퍼에게 타겟을 타워로 매개변수 보낸거 활용하기
    {
        spawnDataRandomValue = Random.Range(0, spawnData);
        spawnPosX = Random.Range(-8, 9);
        spawnPosY = Random.Range(-8, 9);
        Vector3 randPos = new Vector3(monsterSpawnPoint.transform.position.x + spawnPosX, monsterSpawnPoint.transform.position.y, monsterSpawnPoint.transform.position.z + spawnPosY);
        currentMonsterPrefab = LeanPool.Spawn(monsterDatas[spawnDataRandomValue].MonsterPrefab,
            randPos, Quaternion.identity).GetComponent<MonsterTowerKeeper>();
        currentMonsterPrefab.MonsterData = monsterDatas[spawnDataRandomValue];
        
    }
}
