using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ϲ� ����: �Ǽ�, ����, �ڿ�ĳ��
public class WorkerUnit : MonoBehaviour
{
    public enum State
    {
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
        target.CollocateWorker(this);
    }

    public void Repair()
    {

    }

    public void Mine()
    {

    }
}
