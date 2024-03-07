using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ϲ� ����: �Ǽ�, ����, �ڿ�ĳ��
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

    public void Collocate(Installation target)
    {
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
            default:
                break;
        }
    }

    public void Decollocate()
    {
        if(collocatedInstallation != null)
        {
            collocatedInstallation.DecollocateWorker(this);
            collocatedInstallation = null;
        }

        state = State.Idle;
    }
}
