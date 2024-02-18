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
    private List<MonsterDataTest> monsterDatas;
    [SerializeField]
    private GameObject monsterSpawnPoint;

    private MonsterType monsterType;
   // private 
    void Start()
    {

    }

    public void SpawnMonster()
    {
       // var newMonster = Instantiate(monsterPrefab).GetComponent<MonsterTest>();
       // newMonster.MonsterData = monsterDatas[(int)_type];

    }
}
