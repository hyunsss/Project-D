using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Field : Installation
{
    [Serializable]
    private enum ResourceType { Mineral, Gas }
    [SerializeField]
    private ResourceType resourceType;

    [SerializeField]
    private int amountPerSec; //ÃÊ´ç ¾ò´Â ÀÚ¿øÀÇ ¾ç

    protected Canvas canvas;
    protected HpBar hpBar;

    private Coroutine minedCoroutine = null;

    private void Awake()
    {
        type = Type.Field;
        canvas = GetComponentInChildren<Canvas>();
        hpBar = canvas.GetComponentInChildren<HpBar>();
    }

    private void OnEnable()
    {
        canvas.gameObject.SetActive(false);
    }

    public IEnumerator MinedCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            int allAmountPerSec = workers.Count * amountPerSec; //ÀÏ²Û ¼ö ¸¸Å­ ÀÚ¿ø È¹µæ

            if (resourceType == ResourceType.Mineral)
            {
                TestGameManager.Instance.GainMineral(allAmountPerSec);
            }
            else if (resourceType == ResourceType.Gas)
            {
                TestGameManager.Instance.GainGas(allAmountPerSec);
            }
        }
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
