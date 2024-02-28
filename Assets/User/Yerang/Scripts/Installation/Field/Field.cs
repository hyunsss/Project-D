using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : Installation
{
    private Coroutine MineCoroutine;

    Coroutine minedCoroutine = null;

    private void Awake()
    {
        type = Type.Field;
    }

    public IEnumerator MinedCoroutine()
    {
        yield return new WaitForSeconds(1f);

        //¹Ýº¹
        float minedResourcePerSec = 0;
        foreach (WorkerUnit workerUnit in workers)
        {
            minedResourcePerSec += workerUnit.mineSpeed;
        }
        //
    }

    public override void CollocateWorker(WorkerUnit worker)
    {
        base.CollocateWorker(worker);

        print("¹ç ¹èÄ¡µÊ");

        if (minedCoroutine == null)
        {
            minedCoroutine = StartCoroutine(MinedCoroutine());
        }
    }

    public override void DecollocateWorker(WorkerUnit worker)
    {
        base.DecollocateWorker(worker);

        print("¹ç ¹èÄ¡ ÇØÁ¦µÊ");

        if (workers.Count == 0)
        {
            StopCoroutine(minedCoroutine);
        }
    }
}
