using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockWall : Installation
{
    public int price;
    protected Coroutine repairCoroutine = null;

    protected override void Awake()
    {
        base.Awake();
        type = Type.Wall;
    }

    protected virtual void OnEnable()
    {
        /*foreach (WorkerUnit worker in workers)
        {
            DecollocateWorker(worker);
        }*/

        currentHp = maxHp;
        canvas.gameObject.SetActive(false);
        GameDB.Instance.tower_Player.Add(transform);
    }


    public override void CollocateWorker(WorkerUnit worker)
    {
        base.CollocateWorker(worker);

        print("���� ��ġ��");

        if (repairCoroutine == null)
        {
            repairCoroutine = StartCoroutine(Repaired());
            //�� ����Ʈ
        }
    }

    public override void DecollocateWorker(WorkerUnit worker)
    {
        base.DecollocateWorker(worker);

        print("���� ��ġ ������");

        if (workers.Count == 0)
        {
            StopCoroutine(repairCoroutine);
            //�� ����Ʈ ����
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