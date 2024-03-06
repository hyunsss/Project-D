using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: 매니저 만들어서 스폰 관리
public class SpawnTower : Tower
{
    public SpawnTowerInfo towerInfo;

    public float iteration;

    //public GameObject[] characterPrefabs;
    public GameObject characterPrefab;

    private bool isSpawning = false;

    private Transform spawnPoint;

    protected override void Awake()
    {
        base.Awake();
        spawnPoint = transform.GetChild(0); //0: SpawnPoint
    }

    public override void SetInfo()
    {
        //스탯 설정
        this.maxHp = towerInfo.levelStat[level - 1].maxHp;
        this.iteration = towerInfo.levelStat[level - 1].iteration;

        currentHp = maxHp;

        //렌더러 설정
        Destroy(transform.GetChild(3).gameObject); //3: Render
        Instantiate(towerInfo.rendererPrefabs[level - 1], transform);

        StopAllCoroutines();

        hpBar.SetHpBar(currentHp, maxHp);
    }

    public void Spawn(int spawnCount)
    {
        StartCoroutine(SpawnCoroutine(spawnCount));
    }

    private IEnumerator SpawnCoroutine(int spawnCount) //TODO: 순서 꼬이는 문제 있음
    {
        while (isSpawning) yield return null; //스폰중인 상태면 대기

        isSpawning = true;
        //print($"Spawn 코루틴 진입: {spawnCount}");
        for (int i = 0; i < spawnCount; i++)
        {
            yield return new WaitForSeconds(iteration);

            GameObject spawnedCharacter =
                Lean.Pool.LeanPool.Spawn(characterPrefab, spawnPoint.position, transform.rotation,
                UnitManager.Instance.UnitParent);
        }
        isSpawning = false;
        yield break;
    }
}
