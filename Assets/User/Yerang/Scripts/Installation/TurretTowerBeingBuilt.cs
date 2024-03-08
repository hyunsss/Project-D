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

        if (currentTime >= completeTime)
        {
            CompleteBuild();
        }
    }

    public void CompleteBuild()
    {
        Tower completeTower = 
            Lean.Pool.LeanPool.Spawn(tower, transform.position, transform.rotation, InstallationManager.Instance.InstallationParent);
        
        completeTower.SetHp(currentHp);

        //Lean.Pool.LeanPool.Despawn(gameObject);
        Destroy(gameObject);
    }

    public override void CollocateWorker(WorkerUnit worker)
    {
        base.CollocateWorker(worker);

        print("�Ǽ� ��ġ��");
    }

    public override void DecollocateWorker(WorkerUnit worker)
    {
        base.DecollocateWorker(worker);

        print("�Ǽ� ��ġ ������");
    }
}
