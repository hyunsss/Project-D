using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Sword,
    Magic
} 

public class MosterSpawnerTest : MonoBehaviour
{
    [SerializeField]
    private List<MonsterDataTest>   monsterDatas;
    [SerializeField]
    private GameObject              monsterSpawnPoint;
    [SerializeField]
    private MonsterTest             monsterPrefab;

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
        
        MonsterTest newMonster  = Instantiate(monsterDatas[spawnDataRandomValue].MonsterPrefab,
            monsterSpawnPoint.transform).GetComponent<MonsterTest>();

        newMonster.MonsterData = monsterDatas[spawnDataRandomValue];

    }
}
