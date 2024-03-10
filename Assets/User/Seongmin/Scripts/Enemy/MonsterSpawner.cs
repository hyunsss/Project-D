using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Lean.Pool;


public class MonsterSpawner : MonoBehaviour
{
    [Header("Monster_Datas")]
    [SerializeField]
    private List<MonsterData>       monsterDatas;

    [Header("BOSS_Datas")]
    [SerializeField]
    private List<MonsterData>       bossDatas;
    [SerializeField]
    private GameObject              monsterSpawnPoint;
    private Monster                 currentMonsterPrefab;

    private int                     spawnData;
    private int                     spawnDataRandomValue;
    private int                     spawnPosX;
    private int                     spawnPosY;
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
            randPos,Quaternion.identity, gameObject.transform).GetComponent<Monster>();
        currentMonsterPrefab.MonsterData = monsterDatas[spawnDataRandomValue];
        currentMonsterPrefab.state = Monster.State.chase;
        currentMonsterPrefab.SetInfo();

        currentMonsterPrefab.transform.SetParent(GameObject.Find("SeongminManager").transform);

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
            randPos, Quaternion.identity, gameObject.transform).GetComponent<Monster>();

        currentKeeperPrefab.MonsterData = monsterDatas[spawnDataRandomValue];
        currentKeeperPrefab.SetTowerObject(_tower);
        currentKeeperPrefab.state = Monster.State.towerReqair;
        currentMonsterPrefab.SetInfo();

        currentKeeperPrefab.transform.SetParent(GameObject.Find("SeongminManager").transform);
    }

    public void SpawnBossMosnter()
    {
        spawnDataRandomValue = Random.Range(0, bossDatas.Count);
        spawnPosX = Random.Range(-8, 9);
        spawnPosY = Random.Range(-8, 9);

        Vector3 randPos = new Vector3(monsterSpawnPoint.transform.position.x + spawnPosX,
            monsterSpawnPoint.transform.position.y,
            monsterSpawnPoint.transform.position.z + spawnPosY);

        var currentBossPrefab = LeanPool.Spawn(bossDatas[spawnDataRandomValue].MonsterPrefab,
            randPos, Quaternion.identity, gameObject.transform).GetComponent<Monster>();

        currentBossPrefab.MonsterData = bossDatas[spawnDataRandomValue];

        currentBossPrefab.transform.SetParent(GameObject.Find("SeongminManager").transform);
        currentBossPrefab.state = Monster.State.chase;

        // BossPanel setting
        UI_PanelManager.Instance.BossPanelSet();
       
    }
    public void SpawnMonsterTower(GameObject _monsterTower)
    {
        spawnPosX = Random.Range(-20, 20);
        spawnPosY = Random.Range(-20, 20);

        Vector3 randPos = new Vector3(monsterSpawnPoint.transform.position.x + spawnPosX,
            monsterSpawnPoint.transform.position.y,
            monsterSpawnPoint.transform.position.z + spawnPosY);

        LeanPool.Spawn(_monsterTower,randPos, Quaternion.identity);
    }
}
