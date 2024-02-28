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

    private Installation installation;

    private void Update()
    {

    }

    public void Collocate(Installation target)
    {
        installation = target;
        target.CollocateWorker(this);

        switch (installation.type)
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
        if(installation != null)
        {
            installation.DecollocateWorker(this);
            installation = null;
        }

        state = State.Idle;
    }
}
