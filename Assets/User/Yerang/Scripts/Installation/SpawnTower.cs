using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTower : Tower
{
    public SpawnTowerInfo towerInfo;

    public float iteration;
    private Transform spawnPoint;

    [Serializable]
    public struct SpawnableUnit
    {
        public GameObject unitPrefab;
        public int requiredResource;
    }
    public SpawnableUnit[] spawnableUnits;

    private GameObject selectedUnit;
    private int spawnCount;

    //private bool isSpawning = false;
    //private Coroutine currentCoroutine;
    //private List<Coroutine> waitingLine = new List<Coroutine>();

    protected override void Awake()
    {
        base.Awake();
        spawnPoint = transform.Find("SpawnPoint");
    }

    public override void SetTower()
    {
        //���� ����
        this.maxHp = towerInfo.levelStat[level - 1].maxHp;
        this.iteration = towerInfo.levelStat[level - 1].iteration;

        currentHp = maxHp;
        hpBar.SetHpBar(currentHp, maxHp);

        //������ ����
        SetRender();

        StopAllCoroutines();
    }

    protected void SetRender()
    {
        Transform renderParent = transform.Find("Render");
        Destroy(renderParent.GetChild(0).gameObject);
        Instantiate(towerInfo.rendererPrefabs[level - 1], renderParent);
    }

    //-----------//
    public void SelectUnit(int i)
    {
        selectedUnit = spawnableUnits[i].unitPrefab;
        IsCanSpawn();
    }

    public bool IsCanSpawn()
    {
        //��ȭ�� �����ϸ�
        if (!GameDB.Instance.IsEnoughResource(spawnableUnits[spawnCount].requiredResource)) 
        {
            UI_PanelManager.Instance.NoMoneyMessage();
            return false;
        }
        else
        {
            Spawn();
            return true;
        }
    }

    public void Spawn()
    {
        GameDB.Instance.UseReSource(spawnableUnits[spawnCount].requiredResource); //��ȭ ���

        GameObject spawnedUnit =
                Lean.Pool.LeanPool.Spawn(selectedUnit, spawnPoint.position, transform.rotation,
                UnitManager.Instance.UnitParent);
        if (spawnedUnit.TryGetComponent(out BattleUnit _battleUnit)) 
        {
            _battleUnit.maxHp += GameDB.Instance.value_Unit_HP_Level_UP;
            _battleUnit.ap    += GameDB.Instance.value_Unit_Damage_Level_UP;
        }
        else if(spawnedUnit.TryGetComponent(out BattleUnit _unit))
        {
            _unit.maxHp       += GameDB.Instance.value_Unit_HP_Level_UP;
        }

        GameDB.Instance.unit_Player.Add(spawnedUnit.transform);
    }
    //-----------//

    /*public void SetSpawnCount(int count)
    {    
        pawnCount = count;
    }*/

    /*private IEnumerator SpawnCoroutine(int spawnCount, GameObject characterPrefab) //TODO: ���� ���̴� ���� ����
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
    }*/
}
