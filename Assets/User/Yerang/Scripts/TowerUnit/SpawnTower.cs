using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTower : MonoBehaviour
{
    public int maxHp;
    private int currentHp;

    public float iteration;
    public int maxSpawnCount;

    public GameObject characterPrefab;
    private List<GameObject> spawnList = new List<GameObject>();

    private Vector3 spawnPoint;

    private void Awake()
    {
        currentHp = maxHp;

        spawnPoint = transform.localPosition + transform.localRotation 
            * Vector3.forward * 3.5f;
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(iteration);

            if(spawnList.Count < maxSpawnCount)
            {
                GameObject spawnedCharacter =
                    Instantiate(characterPrefab, spawnPoint, transform.rotation,
                    transform.GetChild(0)); //0: SpawnCharacters
                spawnList.Add(spawnedCharacter);
            }
        }
    }
}
