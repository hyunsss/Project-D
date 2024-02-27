using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    private List<WorkerUnit> workers = new List<WorkerUnit>();
    private Coroutine MineCoroutine;

    Coroutine minedCoroutine = null;

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

    public void CollocateWorker(WorkerUnit worker)
    {
        print("¹ç ¹èÄ¡µÊ");
        workers.Add(worker);

        if (minedCoroutine == null)
        {
            minedCoroutine = StartCoroutine(MinedCoroutine());
        }
    }

    public void DecollocateWorker(WorkerUnit worker)
    {
        workers.Remove(worker);

        if (workers.Count == 0)
        {
            StopCoroutine(minedCoroutine);
        }
    }
}
