using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: 매니저 만들어서 스폰 관리
public class SpawnTower : Tower
{
    public float iteration;

    public GameObject characterPrefab;

    private bool isSpawning = false;

    private Transform spawnPoint;
    [SerializeField]
    private Transform spawnParent; //TODO: spawnParent 매니저에서 참조하도록

    private void Awake()
    {
        spawnPoint = transform.GetChild(0); //0: SpawnPoint
    }

    protected override void OnEnable()
    {
        base.OnEnable();
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
                spawnParent);
        }
        isSpawning = false;
        yield break;
    }
}
