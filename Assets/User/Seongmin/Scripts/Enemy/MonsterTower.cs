using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTower : MonoBehaviour
{
    [SerializeField]
    private float towerMaxHp = 100f;
    public float TowerMaxHp { get { return towerMaxHp; } set { towerMaxHp = value; } }
    [SerializeField]
    private float towerCurrentHp;
    public float TowerCurrnetHp { get { return towerCurrentHp; } set { towerCurrentHp = value; } }
    [SerializeField]
    private int keeperMaxCount = 10;
    private int keeperSpawnCount = 0;
    [SerializeField]
    private int monsterCount = 0;
    [SerializeField]
    private int need_Monster_Tower_SpawnCount = 20;

    public GameObject mosterTowerPrefab;

    private MonsterSpawner monsterSpawner;


    private void Awake()
    {
        monsterSpawner = GetComponent<MonsterSpawner>();
    }

    private void Start()
    {
        mosterTowerPrefab = this.gameObject;
        towerCurrentHp = 30f;
        StartCoroutine(spawnMonster());
    }

    // ------------SpawnCoroutine------------
    IEnumerator spawnMonster()
    {

        while (towerCurrentHp > 0)
        {

            MosnterSpawn();

            if (towerCurrentHp < towerMaxHp / 2 && keeperSpawnCount <= keeperMaxCount)
            {
                RepairMonsterSpawn();
            }

            if (monsterCount >= need_Monster_Tower_SpawnCount)
            {
                monsterCount = 0;
                MonsterTowerSpawn();
            }
            yield return new WaitForSeconds(2f);
        }
        yield break;
    }
    public void HitDamage(float _damage)
    {
        towerCurrentHp -= _damage;
        if (towerCurrentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void RepairingTower(float _heal)
    {
        towerCurrentHp += _heal;
    }
    public void MosnterSpawn()
    {
        monsterSpawner.SpawnMonster();
        GameDB.Instance.currentMonsterCount++;
        monsterCount++;

    }
    public void RepairMonsterSpawn()
    {
        monsterSpawner.SpawnTowerKeeper(this);
        GameDB.Instance.currentMonsterCount++;
        keeperSpawnCount++;
        monsterCount++;
    }
    public void MonsterTowerSpawn()
    {
        monsterSpawner.SpawnMonsterTower(mosterTowerPrefab);
        BossSpawn();
    }
    public void BossSpawn()
    {
        monsterSpawner.SpawnBossMosnter();
    }
}
