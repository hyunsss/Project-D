using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBeingBuilt : MonoBehaviour
{
    public Tower tower;

    public float builtSpeed;
    public float completeTime;
    private float currentTime;

    [SerializeField]
    private List<WorkerUnit> workers = new List<WorkerUnit>();

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
            Instantiate(tower);
            Destroy(gameObject);
        }
    }

    public void CollocateWorker(WorkerUnit worker)
    {
        print("°Ç¼³ ¹èÄ¡µÊ");
        workers.Add(worker);
    }

    public void DecollocateWorker(WorkerUnit worker)
    {
        workers.Remove(worker);
    }
}
