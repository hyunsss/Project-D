using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//일꾼 유닛: 건설, 수리, 자원캐기
public class WorkerUnit : MonoBehaviour
{
    public enum State
    {
        Idle,
        Build,
        Repair,
        Mine
    }
    public State state;

    public float buildSpeed;
    public float repairSpeed;
    public float mineSpeed;

    public void Build(TowerBeingBuilt target)
    {
        state = State.Build;
        target.CollocateWorker(this);
    }

    public void Repair(Tower target)
    {
        state = State.Repair;
        target.CollocateWorker(this);
    }

    public void Mine(Field target)
    {
        state = State.Mine;
        target.CollocateWorker(this);
    }
}
