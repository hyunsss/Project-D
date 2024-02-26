using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int areaWidth = 2;
    [SerializeField] private int areaHeight = 2;

    public int AreaWidth { get => areaWidth; }
    public int AreaHeight { get => areaHeight; }

    public float maxHp;
    private float currentHp;

    private List<WorkerUnit> repairWorkers = new List<WorkerUnit>();
    private Coroutine repairCoroutine;

    private void Awake()
    {
        currentHp = maxHp;
    }

    protected virtual void Start() { }

    public void GetDamage(float damage)
    {
        currentHp -= damage;
    }

    public IEnumerator Repaired()
    {
        yield return new WaitForSeconds(1f);

        //¹Ýº¹
        float repairedHpPerSec = 0;
        foreach (WorkerUnit workerUnit in repairWorkers)
        {
            repairedHpPerSec += workerUnit.repairSpeed;
        }
        currentHp += repairedHpPerSec;
    }
    public void CollocateWorker(WorkerUnit worker)
    {
        print("¹èÄ¡µÊ");
        repairWorkers.Add(worker);
    }

    public void Destroyed()
    {
        //To-Do: LeanPool
        Destroy(gameObject);
    }
}
