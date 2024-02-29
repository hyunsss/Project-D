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

        //π›∫π
        //¿Á»≠ ¡ı∞°
        float minedResourcePerSec = 0;
        foreach (WorkerUnit workerUnit in workers)
        {
            minedResourcePerSec += workerUnit.mineSpeed;
        }
    }

    public override void CollocateWorker(WorkerUnit worker)
    {
        base.CollocateWorker(worker);

        print("πÁ πËƒ°µ ");

        if (minedCoroutine == null)
        {
            minedCoroutine = StartCoroutine(MinedCoroutine());
        }
    }

    public override void DecollocateWorker(WorkerUnit worker)
    {
        base.DecollocateWorker(worker);

        print("πÁ πËƒ° «ÿ¡¶µ ");

        if (workers.Count == 0)
        {
            StopCoroutine(minedCoroutine);
        }
    }
}
