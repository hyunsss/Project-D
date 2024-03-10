using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class TurretTowerBeingBuilt : Installation
{
    public TurretTower tower;

    public float builtSpeed;
    public float completeTime;
    private float currentTime;

    protected override void Awake()
    {
        base.Awake();
        type = Type.TowerBeingBuilt;
        maxHp = tower.towerInfo.levelStat[0].maxHp;
    }

    private void OnEnable()
    {
        currentTime = 0f;
        currentHp = maxHp;
        hpBar.SetHpBar(currentHp, maxHp);

        GameDB.Instance.tower_Player.Add(transform);
        canvas.gameObject.SetActive(false);
    }

    private void OnDisable() {
        GameDB.Instance.tower_Player.Remove(transform);
    }

    private void Update()
    {
        float surportedTime = 0;
        foreach (WorkerUnit workerUnit in workers)
        {
            surportedTime += Time.deltaTime * workerUnit.buildSpeed;
        }
        currentTime += (Time.deltaTime * builtSpeed + surportedTime);
        progressBar.FillAmount(currentTime / completeTime);
        if (currentTime >= completeTime)
        {
            CompleteBuild();
        }
    }

    public void CompleteBuild() //머지용 주석
    {
        //배치되어 있던 일꾼 모두 해제
        for (int i = 0; i < workers.Count; i++)
        {
            workers[i].Decollocate();
        }

        //타워 생성
        Tower completeTower = 
            Lean.Pool.LeanPool.Spawn(tower, transform.position, transform.rotation, InstallationManager.Instance.InstallationParent);
        
        completeTower.SetHp(currentHp);
        if (completeTower.TryGetComponent(out TowerAttack _towerAttack) && completeTower.TryGetComponent(out Installation _tower))
        {
            _towerAttack.Damage += GameDB.Instance.value_Tower_Damgae_Level_UP;
            _tower.maxHp += GameDB.Instance.value_Tower_HP_Level_UP;
        }
        
        PlayerData.Instance.BuildCount += 1;
        Lean.Pool.LeanPool.Despawn(this);
    }

    public override void CollocateWorker(WorkerUnit worker)
    {
        base.CollocateWorker(worker);

        print("건설 배치됨");
    }

    public override void DecollocateWorker(WorkerUnit worker)
    {
        base.DecollocateWorker(worker);

        print("건설 배치 해제됨");
    }
}
