using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTowerBeingBuilt : Installation
{
    public SpawnTower tower;

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

        /*foreach (WorkerUnit worker in workers)
        {
            DecollocateWorker(worker);
        }*/
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

    public void CompleteBuild()
    {
        //배치되어 있던 일꾼 모두 해제
        /*foreach (WorkerUnit worker in workers)
        {
            worker.Decollocate();
        }*/

        //타워 생성
        Tower completeTower =
            Lean.Pool.LeanPool.Spawn(tower, transform.position, transform.rotation, InstallationManager.Instance.InstallationParent);

        completeTower.SetHp(currentHp);
        //Lean.Pool.LeanPool.Despawn(gameObject);
        Destroy(gameObject);
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
