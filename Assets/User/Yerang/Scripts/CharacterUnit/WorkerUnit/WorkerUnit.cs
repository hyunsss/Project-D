using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//일꾼 유닛: 건설, 수리, 자원캐기
public class WorkerUnit : Unit
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
    public float repairAmount;
    public int mineAmount;

    private Installation belongInstallation;

    public void Collocate(Installation target)
    {
        belongInstallation = target;
        target.CollocateWorker(this);

        switch (belongInstallation.type)
        {
            case Installation.Type.Tower:
                state = State.Repair;
                break;

            case Installation.Type.TowerBeingBuilt:
                state = State.Build;
                break;

            case Installation.Type.Field:
                state = State.Mine;
                break;
            default:
                break;
        }
    }

    public void Decollocate()
    {
        if(belongInstallation != null)
        {
            belongInstallation.DecollocateWorker(this);
            belongInstallation = null;
        }

        state = State.Idle;
    }
}
