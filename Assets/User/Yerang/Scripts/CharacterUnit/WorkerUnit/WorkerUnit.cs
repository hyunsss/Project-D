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

    private Installation collocatedInstallation;
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponentInChildren<Animator>();
    }

    public void Collocate(Installation target)
    {
        //animator.SetBool("work", true);
        collocatedInstallation = target;
        target.CollocateWorker(this);

        switch (collocatedInstallation.type)
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
            case Installation.Type.Wall:
                state = State.Repair;
                break;
            default:
                break;
        }
    }

    public void Decollocate()
    {
        //animator.SetBool("work", false);
        if (collocatedInstallation != null)
        {
            collocatedInstallation.DecollocateWorker(this);
            collocatedInstallation = null;
        }

        state = State.Idle;
    }
}
