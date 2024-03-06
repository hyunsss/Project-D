using UnityEngine;

//알아서 가까운 타겟 찾아 이동
//수동으로 이동 시키기
public class BattleUnitMove : UnitMove
{
    public float detectingRange;
    private float attackRange;

    private Transform target;

    protected override void Awake()
    {
        base.Awake();

        attackRange = GetComponent<BattleUnit>().attackRange;
    }

    protected override void Update()
    {
        DetectEnemy();

        base.Update();
    }

    private void DetectEnemy()
    {
        if (priorityTarget != null) //우선으로 타겟할 대상이 있으면 priorityTarget을 target으로 지정하고 빠져나감
        {
            target = priorityTarget;
            return;
        } 

        int enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, detectingRange, enemyLayerMask);

        float minDis = 999;
        Transform nearTarget = null;

        foreach (Collider collider in detectedColliders)
        {
            float dis = Vector3.Distance(collider.transform.position, transform.position);
            if (dis < minDis)
            {
                minDis = dis;
                nearTarget = collider.transform;
            }
        }

        if (minDis > attackRange)
        {
            target = nearTarget;
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectingRange);
    }

    public override void SetPriorityTarget(Transform target) //ArrowDrawer로 설정
    {
        if (priorityTarget != null)
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

        if (target != null)
        {
            target = null;
        }
    }

    protected override void MoveToTarget()
    {
        //타겟이 적일 경우 사정거리 안에 들어올 때 까지만 이동
        if (target.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (nav.stoppingDistance != attackRange - 0.1f)
                nav.stoppingDistance = attackRange - 0.1f;
        }
        else if (priorityTarget.gameObject.layer == LayerMask.NameToLayer("Installation"))
        { //타겟이 건물인 경우 타겟의 인접점까지 이동
            Collider targetCollider = priorityTarget.GetComponent<Collider>();
            //접점
            Vector3 tangentPoint = targetCollider.ClosestPoint(transform.position);

            nav.stoppingDistance = 1.0f;
            nav.SetDestination(tangentPoint);
        }
        //아닐 경우 끝까지 이동
        else
        {
            if (nav.stoppingDistance != 0f)
                nav.stoppingDistance = 0f;
        }

        nav.SetDestination(target.position);

        if (nav.velocity.sqrMagnitude >= 0.1f //길찾기 시작할때도 남은 거리가 0으로 뜨게되므로, 움직이는 상태인지 체크
            && nav.remainingDistance <= nav.stoppingDistance + 0.1f)
        {
            ResetTarget();
        }

        //TODO: 장애물에 막혀 이동이 불가한 상태가 지속될 경우?
    }
}