using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTower : Tower
{
    public float iteration;

    public GameObject characterPrefab;

    private Vector3 spawnPoint;

    private bool isSpawning = false;

    protected override void Start()
    {
        spawnPoint = transform.localPosition + transform.localRotation 
            * Vector3.forward * 3.5f;
    }

    public void Spawn(int spawnCount)
    {
        StartCoroutine(SpawnCoroutine(spawnCount));
    }

    private IEnumerator SpawnCoroutine(int spawnCount)
    {
        while (isSpawning) yield return null; //스폰중인 상태면 대기

        isSpawning = true;
        print($"Spawn 코루틴 진입: {spawnCount}");
        for (int i = 0; i < spawnCount; i++)
        {
            yield return new WaitForSeconds(iteration);

            GameObject spawnedCharacter =
                Instantiate(characterPrefab, spawnPoint, transform.rotation,
                transform.GetChild(0)); //0: SpawnCharacters
        }
        isSpawning = false;
        yield break;
    }
}
