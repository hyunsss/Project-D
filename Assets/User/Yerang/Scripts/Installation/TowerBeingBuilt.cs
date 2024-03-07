using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBeingBuilt : Installation
{
    public Tower tower;

    public float builtSpeed;
    public float completeTime;
    private float currentTime;

    protected override void Awake()
    {
        base.Awake();
        type = Type.TowerBeingBuilt;
        maxHp = tower.MaxHp; //업그레이드해서 체력이 올라간 상태면? -> 부실 때 다 초기화해주기
    }

    private void OnEnable()
    {
        currentTime = 0f;
        currentHp = maxHp;
        canvas.gameObject.SetActive(false);
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

        Lean.Pool.LeanPool.Despawn(gameObject);
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
