using System;
using System.Collections;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Field : Installation
{
    public FieldInfo fieldInfo;

    public int level;

    [SerializeField]
    private int amountPerSec; //�ʴ� ��� �ڿ��� ��;

    private Coroutine minedCoroutine = null;

    protected override void Awake()
    {
        base.Awake();
        type = Type.Field;
    }

    private void OnEnable()
    {
        canvas.gameObject.SetActive(false);
        GameDB.Instance.tower_Player.Add(transform);
    }

    public void SetField()
    {
        //���� ����
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

            int allAmountPerSec = workers.Count * amountPerSec; //�ϲ� �� ��ŭ �ڿ� ȹ��

            GameDB.Instance.GainMineral(allAmountPerSec);
        }
    }

    public override void CollocateWorker(WorkerUnit worker)
    {
        base.CollocateWorker(worker);

        //print("�� ��ġ��");

        if (minedCoroutine == null)
        {
            minedCoroutine = StartCoroutine(MinedCoroutine());
        }
    }

    public override void DecollocateWorker(WorkerUnit worker)
    {
        base.DecollocateWorker(worker);

        //print("�� ��ġ ������");

        if (workers.Count == 0)
        {
            StopCoroutine(minedCoroutine);
        }
    }
}
