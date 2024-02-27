using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected int areaWidth = 2;
    [SerializeField] protected int areaHeight = 2;

    public int AreaWidth { get => areaWidth; }
    public int AreaHeight { get => areaHeight; }

    public int level;

    public float maxHp;
    protected float currentHp;

    protected List<WorkerUnit> repairWorkers = new List<WorkerUnit>();
    protected Coroutine repairCoroutine = null;

    private void Awake()
    {
        currentHp = maxHp;
    }

    protected virtual void Start() { }

    public void GetDamage(float damage)
    {
        currentHp -= damage;

        if(currentHp <= 0)
        {
            Destroyed();
        }
    }

    public IEnumerator Repaired()
    {
        yield return new WaitForSeconds(1f);

        //반복
        float repairedHpPerSec = 0;
        foreach (WorkerUnit workerUnit in repairWorkers)
        {
            repairedHpPerSec += workerUnit.repairSpeed;
        }
        currentHp += repairedHpPerSec;
    }

    public void CollocateWorker(WorkerUnit worker)
    {
        print("수리 배치됨");
        repairWorkers.Add(worker);

        if(repairCoroutine == null)
        {
            repairCoroutine = StartCoroutine(Repaired());
        }
    }

    public void DecollocateWorker(WorkerUnit worker)
    {
        repairWorkers.Remove(worker);

        if(repairWorkers.Count == 0)
        {
            StopCoroutine(repairCoroutine);
        }
    }

    public void Destroyed()
    {
        //To-Do: LeanPool
        Destroy(gameObject);
    }
}
