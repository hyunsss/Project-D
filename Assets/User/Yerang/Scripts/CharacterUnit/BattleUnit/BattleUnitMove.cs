using UnityEngine;
using UnityEngine.AI;

//�˾Ƽ� ����� Ÿ�� ã�� �̵�
//�������� �̵� ��Ű��
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
    
    private void OnEnable()
    {
        Vector3 agentStartPosition = transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(agentStartPosition, out hit, 10.0f, NavMesh.AllAreas))
        {
            nav.transform.position = hit.position;
            nav.enabled = true;
        }
        else
        {
            Debug.LogWarning("Failed to place the agent on a NavMesh.");
        }
    }

    protected void Update()
    {
        DetectEnemy();

        switch (state)
        {
            case State.Idle:
                if (priorityTarget != null || target != null)
                {
                    state = State.Move;
                }
                break;

            case State.Move:
                if (priorityTarget == null && target == null)
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

    private void DetectEnemy()
    {
        if (priorityTarget != null) //�켱���� Ÿ���� ����� ������ priorityTarget�� target���� �����ϰ� ��������
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

    public override void SetPriorityTarget(Transform target) //ArrowDrawer�� ����
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
        if (target == null && priorityTarget == null) return; 
        //Ÿ���� ���� ��� �����Ÿ� �ȿ� ���� �� ������ �̵�
        if (target.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            nav.stoppingDistance = attackRange - 0.1f;
            nav.SetDestination(target.position);
        }
        //Ÿ���� �ǹ��� ��� Ÿ���� ���������� �̵�
        else if (priorityTarget.gameObject.layer == LayerMask.NameToLayer("Installation"))
        { 
            Collider targetCollider = priorityTarget.GetComponent<Collider>();
            //����
            Vector3 tangentPoint = targetCollider.ClosestPoint(transform.position);

            nav.stoppingDistance = 1.0f;
            nav.SetDestination(tangentPoint);
        }
        //�ƴ� ��� ������ �̵�
        else
        {
            Debug.Log(target);
            nav.stoppingDistance = 1f;
            Debug.Log(target.position);
            Debug.Log("Is Agent on NavMesh? " + nav.isOnNavMesh);
            nav.SetDestination(target.position);
        }

        if (nav.velocity.sqrMagnitude >= 0.1f //��ã�� �����Ҷ��� ���� �Ÿ��� 0���� �߰ԵǹǷ�, �����̴� �������� üũ
            && nav.remainingDistance <= nav.stoppingDistance + 0.1f)
        {
            ResetTarget();
        }
    }
}