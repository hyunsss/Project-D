using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
    public List<GameObject> createList = new List<GameObject>();
    private int spawnCount;

    float currentTime;
    float startTime;
    bool isGet;

    //private bool isSpawning = false;
    //private Coroutine currentCoroutine;
    //private List<Coroutine> waitingLine = new List<Coroutine>();

    protected override void Awake()
    {
        base.Awake();
        spawnPoint = transform.Find("SpawnPoint");
    }


    private void Update() {
        if(selectedUnit == null && createList.Count != 0) {
            selectedUnit = createList[0];
            isGet = false;
        }

        if(selectedUnit != null) {
            GetStartTime();
            currentTime = Time.time - startTime;
            if(progressBar.gameObject.activeSelf == false) progressBar.gameObject.SetActive(true);
            progressBar.FillAmount(currentTime / towerInfo.levelStat[level].iteration);

            if(currentTime > towerInfo.levelStat[level].iteration) {
                Spawn();
                createList.RemoveAt(0);
                selectedUnit = null;
                progressBar.FillReset();
                progressBar.gameObject.SetActive(false);
                
            }
        }
    }

    public void CancelSpawnUnit(int i) {
        if(createList.Count <= i) return;
        if(createList.Count == 0) return;

        GameDB.Instance.GainMineral(createList[i].GetComponent<Unit>().price);
        createList.RemoveAt(i);
        if(i == 0) {
            selectedUnit = null;
            progressBar.FillReset();
            progressBar.gameObject.SetActive(false);
        }
    }

    private void GetStartTime() {
        if(isGet == false) startTime = Time.time;
        isGet = true;
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
        if(IsCanSpawn(i) == false) return;
        if(createList.Count > 4) Debug.Log("더 이상 선택할 수 없습니다.");
        else {
            GameDB.Instance.UseReSource(spawnableUnits[i].unitPrefab.GetComponent<Unit>().price);

            createList.Add(spawnableUnits[i].unitPrefab);

        }
    }

    public bool IsCanSpawn(int i)
    {
        if (!GameDB.Instance.IsEnoughResource(spawnableUnits[i].unitPrefab.GetComponent<Unit>().price)) 
        {
            UI_PanelManager.Instance.NoMoneyMessage();
            return false;
        }

        return true;
    }

    public void Spawn()
    {
        GameObject spawnedUnit =
                Lean.Pool.LeanPool.Spawn(selectedUnit, spawnPoint.position, transform.rotation,
                UnitManager.Instance.UnitParent);
        if (spawnedUnit.TryGetComponent(out BattleUnit _battleUnit))
        {
            _battleUnit.maxHp += GameDB.Instance.value_Unit_HP_Level_UP;
            _battleUnit.ap += GameDB.Instance.value_Unit_Damage_Level_UP;
        }
        else if (spawnedUnit.TryGetComponent(out BattleUnit _unit))
        {
            _unit.maxHp += GameDB.Instance.value_Unit_HP_Level_UP;
        }
        GameDB.Instance.unit_Player.Add(spawnedUnit.transform);

        PlayerData.Instance.UnitbuildCount += 1;
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
