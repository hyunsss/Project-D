using System;
using System.Collections;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Field : Installation
{
    public FieldInfo fieldInfo;

    public int level;

    [SerializeField]
    private int amountPerSec; //ÃÊ´ç ¾ò´Â ÀÚ¿øÀÇ ¾ç;

    private Coroutine minedCoroutine = null;

    protected override void Awake()
    {
        base.Awake();
        type = Type.Field;
    }

    private void OnEnable()
    {
        /*foreach (WorkerUnit worker in workers)
        {
            DecollocateWorker(worker);
        }*/

        canvas.gameObject.SetActive(false);
    }

    public void SetField()
    {
        //½ºÅÈ ¼³Á¤
        this.maxHp = fieldInfo.levelStat[level - 1].maxHp;
        this.amountPerSec = fieldInfo.levelStat[level - 1].amountPerSec;

        currentHp = maxHp;
        hpBar.SetHpBar(currentHp, maxHp);

        StopAllCoroutines();
    }

    public IEnumerator MinedCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            int allAmountPerSec = workers.Count * amountPerSec; //ÀÏ²Û ¼ö ¸¸Å­ ÀÚ¿ø È¹µæ

            GameDB.Instance.GainMineral(allAmountPerSec);
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
