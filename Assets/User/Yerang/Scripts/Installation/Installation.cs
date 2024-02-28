using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Installation : MonoBehaviour
{
    public enum Type
    {
        Tower,
        TowerBeingBuilt,
        Field
    }
    public Type type;

    [SerializeField]
    protected List<WorkerUnit> workers = new List<WorkerUnit>();
    public List<WorkerUnit> Workers { get; }

    public virtual void CollocateWorker(WorkerUnit worker)
    {
        workers.Add(worker);
    }

    public virtual void DecollocateWorker(WorkerUnit worker)
    {
        workers.Remove(worker);
    }
}
