using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBeingBuilt : Installation
{
    public Tower tower;

    public float builtSpeed;
    public float completeTime;
    private float currentTime;

    private void Awake()
    {
        type = Type.TowerBeingBuilt;
    }

    private void Start()
    {
        currentTime = Time.time;
    }

    private void Update()
    {
        float surportedTime = 0;
        foreach (WorkerUnit workerUnit in workers)
        {
            surportedTime += Time.deltaTime * workerUnit.buildSpeed;
        }
        currentTime += (Time.deltaTime * builtSpeed + surportedTime);

        if (currentTime >= completeTime)
        {
            Instantiate(tower, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public override void CollocateWorker(WorkerUnit worker)
    {
        base.CollocateWorker(worker);

        print("건설 배치됨");
    }

    public override void DecollocateWorker(WorkerUnit worker)
    {
        base.DecollocateWorker(worker);

        print("건설 배치 해제됨");
    }
}
