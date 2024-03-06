using UnityEngine;

public class WorkerUnitMove : UnitMove
{
    private WorkerUnit workerUnit; //음...


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
        else //타겟이 골 포인트가 아닐경우 타겟의 인접점까지 이동
        {
            Collider targetCollider = priorityTarget.GetComponent<Collider>();
            //접점
            Vector3 tangentPoint = targetCollider.ClosestPoint(transform.position);

            nav.stoppingDistance = 1.0f;
            nav.SetDestination(tangentPoint);
        }
        
        if (nav.velocity.sqrMagnitude >= 0.1f //길찾기 시작할때도 남은 거리가 0으로 뜨게되므로, 움직이는 상태인지 체크
            && nav.remainingDistance <= nav.stoppingDistance + 0.1f)
        {
            if (priorityTarget.gameObject.layer != LayerMask.NameToLayer("GoalPoint"))
                workerUnit.Collocate(priorityTarget.GetComponent<Installation>());

            ResetTarget();
        }
    }
}
