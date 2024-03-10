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

        print("¼ö¸® ¹èÄ¡µÊ");

        if (repairCoroutine == null)
        {
            repairCoroutine = StartCoroutine(Repaired());
            //Èú ÀÌÆåÆ®
        }
    }

    public override void DecollocateWorker(WorkerUnit worker)
    {
        base.DecollocateWorker(worker);

        print("¼ö¸® ¹èÄ¡ ÇØÁ¦µÊ");

        if (workers.Count == 0)
        {
            StopCoroutine(repairCoroutine);
            //Èú ÀÌÆåÆ® ²ô±â
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