using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTower : Tower
{
    public float iteration;
    public int spawnCount;

    public GameObject characterPrefab;

    private Vector3 spawnPoint;

    protected override void Start()
    {
        print("Start");

        spawnPoint = transform.localPosition + transform.localRotation 
            * Vector3.forward * 3.5f;

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            yield return new WaitForSeconds(iteration);

            GameObject spawnedCharacter =
                Instantiate(characterPrefab, spawnPoint, transform.rotation,
                transform.GetChild(0)); //0: SpawnCharacters
        }

        spawnCount = 0;
    }
}
