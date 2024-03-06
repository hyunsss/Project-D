using System.Collections;
using UnityEngine;

public abstract class Tower : Installation
{
    public int level;

    protected float maxHp;
    protected float currentHp;
    public float CurrentHp {  get { return currentHp; } }

    protected bool isCanUpgrade;
    public bool IsCanUpgrade { get { return isCanUpgrade; } }

    protected Coroutine repairCoroutine = null;

    protected HpBar hpBar;


    protected virtual void Awake()
    {
        type = Type.Tower;
        Canvas canvas = GetComponentInChildren<Canvas>();
        hpBar = GetComponentInChildren<Canvas>().GetComponentInChildren<HpBar>();
        canvas.gameObject.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        level = 1;
        currentHp = maxHp;
        isCanUpgrade = true;
    }

    public abstract void SetInfo();

    public void GetDamage(float damage)
    {
        currentHp -= damage;

        if(currentHp <= 0)
        {
            Destroyed();
        }

        hpBar.SetHpBar(currentHp, maxHp);
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

    public void Destroyed()
    {
        Lean.Pool.LeanPool.Despawn(gameObject);
    }
}
