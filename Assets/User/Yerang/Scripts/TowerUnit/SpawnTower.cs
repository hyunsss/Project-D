using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTower : Tower
{
    public float iteration;

    public GameObject characterPrefab;

    private Vector3 spawnPoint;

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
        print("½ºÆù");
        for (int i = 0; i < spawnCount; i++)
        {
            yield return new WaitForSeconds(iteration);

            GameObject spawnedCharacter =
                Instantiate(characterPrefab, spawnPoint, transform.rotation,
                transform.GetChild(0)); //0: SpawnCharacters
        }
        yield break;
    }
}
