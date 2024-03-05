using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : Installation
{
    [Serializable]
    private enum ResourceType { Mineral, Gas }
    [SerializeField]
    private ResourceType resourceType;

    private Coroutine minedCoroutine = null;

    private void Awake()
    {
        type = Type.Field;
    }

    public IEnumerator MinedCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            //재화 증가
            int minedResourcePerSec = 0; //초당 얻는 자원수
            foreach (WorkerUnit workerUnit in workers)
            {
                minedResourcePerSec += workerUnit.mineAmount;
            }

            if (resourceType == ResourceType.Mineral)
            {
                TestGameManager.Instance.GainMineral(minedResourcePerSec);
            }
            else if (resourceType == ResourceType.Gas)
            {
                TestGameManager.Instance.GainGas(minedResourcePerSec);
            }
        }
    }

    public override void CollocateWorker(WorkerUnit worker)
    {
        base.CollocateWorker(worker);

        print("밭 배치됨");

        if (minedCoroutine == null)
        {
            minedCoroutine = StartCoroutine(MinedCoroutine());
        }
    }

    public override void DecollocateWorker(WorkerUnit worker)
    {
        base.DecollocateWorker(worker);

        print("밭 배치 해제됨");

        if (workers.Count == 0)
        {
            StopCoroutine(minedCoroutine);
        }
    }
}
