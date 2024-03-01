using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Installation
{
    public int level;

    public float maxHp;
    protected float currentHp;

    protected Coroutine repairCoroutine = null;

    protected virtual void OnEnable()
    {
        currentHp = maxHp;
        type = Type.Tower;
    }

    public void GetDamage(float damage)
    {
        currentHp -= damage;

        if(currentHp <= 0)
        {
            Destroyed();
        }
    }

    public IEnumerator Repaired()
    {
        yield return new WaitForSeconds(1f);

        float repairedHpPerSec = 0;
        foreach (WorkerUnit workerUnit in workers)
        {
            repairedHpPerSec += workerUnit.repairSpeed;
        }
        currentHp += repairedHpPerSec;
    }

    public override void CollocateWorker(WorkerUnit worker)
    {
        base.CollocateWorker(worker);

        print("수리 배치됨");

        if(repairCoroutine == null)
        {
            repairCoroutine = StartCoroutine(Repaired());
            //힐 이펙트
        }
    }

    public override void DecollocateWorker(WorkerUnit worker)
    {
        base.DecollocateWorker(worker);

        print("수리 배치 해제됨");

        if(workers.Count == 0)
        {
            StopCoroutine(repairCoroutine);
            //힐 이펙트 끄기
        }
    }

    public void Destroyed()
    {
        Lean.Pool.LeanPool.Despawn(gameObject);
    }
}
