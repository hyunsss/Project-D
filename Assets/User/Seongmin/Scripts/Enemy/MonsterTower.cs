using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTower : MonoBehaviour
{
    [SerializeField]
    private float               towerMaxHp = 100f;
    public float                TowerMaxHp { get { return towerMaxHp; } set { towerMaxHp = value; } }
    [SerializeField]
    private float               towerCurrentHp;
    public float                TowerCurrnetHp { get { return towerCurrentHp; } set { towerCurrentHp = value; } }
    [SerializeField]
    private int                 keeperMaxCount = 10;
    private int                 keeperSpawnCount = 0;
    public int                  monsterCount = 0;
    private MonsterSpawner      monsterSpawner;


    private void Awake()
    {
        monsterSpawner = GetComponent<MonsterSpawner>();       
    }

    private void Start()
    {
        towerCurrentHp = 30f;
        StartCoroutine(spawnMonster());
    }
    IEnumerator spawnMonster()
    {
        
        while(towerCurrentHp > 0)
        {
            GameDB.Instance.monsterCount++;
            monsterSpawner.SpawnMonster();
            if(towerCurrentHp < towerMaxHp / 2 && keeperSpawnCount <= keeperMaxCount)
            {
                GameDB.Instance.monsterCount++;
                keeperSpawnCount++;
                print("Keeper가 소환 되었습니다 ! ");
                monsterSpawner.SpawnTowerKeeper(this);
            }
            yield return new WaitForSeconds(2f);
        }
        yield break;
    }
    public void HitDamage(float _damage)
    {
        towerCurrentHp -= _damage;
        if(towerCurrentHp <= 0) 
        {
            
        }
    }
    public void RepairingTower(float _heal)
    {
        towerCurrentHp += _heal;
    }
}
