using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: ¸Å´ÏÀú ¸¸µé¾î¼­ ½ºÆù °ü¸®
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
        //Å¸ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½Â¦ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½Ä¡ï¿½ï¿½
        spawnPoint = transform.localPosition + transform.localRotation 
            * Vector3.left * 1f;
    }

    public void Spawn(int spawnCount)
    {
        StartCoroutine(SpawnCoroutine(spawnCount));
    }

    private IEnumerator SpawnCoroutine(int spawnCount) //TODO: ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½Ì´ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
    {
        while (isSpawning) yield return null; //ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½Â¸ï¿½ ï¿½ï¿½ï¿½

        isSpawning = true;
        //print($"Spawn ï¿½Ú·ï¿½Æ¾ ï¿½ï¿½ï¿½ï¿½: {spawnCount}");
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
