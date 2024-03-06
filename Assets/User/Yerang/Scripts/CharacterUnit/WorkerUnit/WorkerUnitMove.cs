using UnityEngine;

public class WorkerUnitMove : UnitMove
{
    private WorkerUnit workerUnit; //��...


    protected override void Awake()
    {
        base.Awake();

        workerUnit = GetComponent<WorkerUnit>();
    }

    protected void Update()
    {
        switch (state)
        {
            case State.Idle:
                if (priorityTarget != null)
                {
                    state = State.Move;
                }
                break;

            case State.Move:
                if (priorityTarget == null)
                {
                    state = State.Idle;
                    break;
                }
                MoveToTarget();
                break;

            default:
                break;
        }

        animator.SetInteger("moveState", (int)state);
    }

    public override void SetPriorityTarget(Transform target)
    {
        if (target != null)
        {
            ResetTarget();
        }

        this.priorityTarget = target;
    }

    protected override void ResetTarget()
    {
        if (priorityTarget != null)
        {
            if (priorityTarget.gameObject.layer == LayerMask.NameToLayer("GoalPoint"))
            {
                Lean.Pool.LeanPool.Despawn(priorityTarget.gameObject);
                priorityTarget = null;
            }

            else
                priorityTarget = null;
        }
    }

    protected override void MoveToTarget()
    {
        if (workerUnit.state != WorkerUnit.State.Idle)
        {
            workerUnit.Decollocate();
        }

        if (priorityTarget.gameObject.layer == LayerMask.NameToLayer("GoalPoint"))
        {
            nav.stoppingDistance = 0f;
            nav.SetDestination(priorityTarget.position);

        }
        else //Ÿ���� �� ����Ʈ�� �ƴҰ�� Ÿ���� ���������� �̵�
        {
            Collider targetCollider = priorityTarget.GetComponent<Collider>();
            //����
            Vector3 tangentPoint = targetCollider.ClosestPoint(transform.position);

            nav.stoppingDistance = 1.0f;
            nav.SetDestination(tangentPoint);
        }
        
        if (nav.velocity.sqrMagnitude >= 0.1f //��ã�� �����Ҷ��� ���� �Ÿ��� 0���� �߰ԵǹǷ�, �����̴� �������� üũ
            && nav.remainingDistance <= nav.stoppingDistance + 0.1f)
        {
            if (priorityTarget.gameObject.layer != LayerMask.NameToLayer("GoalPoint"))
                workerUnit.Collocate(priorityTarget.GetComponent<Installation>());

            ResetTarget();
        }
    }
}
