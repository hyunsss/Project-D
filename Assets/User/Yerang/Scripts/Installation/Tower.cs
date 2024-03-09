using System.Collections;
using UnityEngine;

public abstract class Tower : Installation
{
    public int level;

    protected Coroutine repairCoroutine = null;

    protected override void Awake()
    {
        base.Awake();
        type = Type.Tower;
    }

    protected virtual void OnEnable()
    {
        /*foreach (WorkerUnit worker in workers)
        {
            DecollocateWorker(worker);
        }*/

        level = 1;
        SetTower();
        GameDB.Instance.tower_Player.Add(transform);
        canvas.gameObject.SetActive(false);
    }

    public void SetHp(float hp)
    {
        currentHp = hp;
        //hpBar.SetHpBar(currentHp, maxHp);
    }

    public abstract void SetTower();

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

    public IEnumerator Repaired()
    {
        yield return new WaitForSeconds(1f);

        float repairedHpPerSec = 0;
        foreach (WorkerUnit workerUnit in workers)
        {
            repairedHpPerSec += workerUnit.repairAmount;
        }
        currentHp += repairedHpPerSec;
    }
}
