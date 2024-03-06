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

            //��ȭ ����
            int minedResourcePerSec = 0; //�ʴ� ��� �ڿ���
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

        print("�� ��ġ��");

        if (minedCoroutine == null)
        {
            minedCoroutine = StartCoroutine(MinedCoroutine());
        }
    }

    public override void DecollocateWorker(WorkerUnit worker)
    {
        base.DecollocateWorker(worker);

        print("�� ��ġ ������");

        if (workers.Count == 0)
        {
            StopCoroutine(minedCoroutine);
        }
    }
}
