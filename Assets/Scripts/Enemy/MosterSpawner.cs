using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Sword,
    Magic
} 

public class MosterSpawner : MonoBehaviour
{
    [SerializeField]
    private List<MonsterData>       monsterDatas;
    [SerializeField]
    private GameObject              monsterSpawnPoint;
    [SerializeField]
    private Monster                 currentMonsterPrefab;

    private MonsterType             monsterType;

    private int                     spawnData;
    private int                     spawnDataRandomValue;
    
    // private 
    private void Awake()
    {
        spawnData = monsterDatas.Count;

    }
    public void SpawnMonster()
    {
        spawnDataRandomValue = Random.Range(0, spawnData);

        currentMonsterPrefab = Instantiate(monsterDatas[spawnDataRandomValue].MonsterPrefab,
            monsterSpawnPoint.transform).GetComponent<Monster>();

        currentMonsterPrefab.MonsterData = monsterDatas[spawnDataRandomValue];

    }
}
