using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: 매니저 만들어서 스폰 관리
public class SpawnTower : Tower
{
    public float iteration;

    public GameObject characterPrefab;

    private Vector3 spawnPoint;

    private bool isSpawning = false;

    public Transform spawnParent;

    protected override void OnEnable()
    {
        base.OnEnable();
        //타워보다 살짝 앞쪽을 스폰 위치로
        spawnPoint = transform.localPosition + transform.localRotation 
            * Vector3.forward * 3.5f;
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
                Lean.Pool.LeanPool.Spawn(characterPrefab, spawnPoint, transform.rotation,
                spawnParent); //0: SpawnCharacters
        }
        isSpawning = false;
        yield break;
    }
}
