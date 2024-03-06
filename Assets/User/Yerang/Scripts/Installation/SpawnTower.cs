using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: �Ŵ��� ���� ���� ����
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
        //���� ����
        this.maxHp = towerInfo.levelStat[level - 1].maxHp;
        this.iteration = towerInfo.levelStat[level - 1].iteration;

        currentHp = maxHp;

        //������ ����
        Destroy(transform.GetChild(3).gameObject); //3: Render
        Instantiate(towerInfo.rendererPrefabs[level - 1], transform);

        StopAllCoroutines();

        hpBar.SetHpBar(currentHp, maxHp);
    }

    public void Spawn(int spawnCount)
    {
        StartCoroutine(SpawnCoroutine(spawnCount));
    }

    private IEnumerator SpawnCoroutine(int spawnCount) //TODO: ���� ���̴� ���� ����
    {
        while (isSpawning) yield return null; //�������� ���¸� ���

        isSpawning = true;
        //print($"Spawn �ڷ�ƾ ����: {spawnCount}");
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
