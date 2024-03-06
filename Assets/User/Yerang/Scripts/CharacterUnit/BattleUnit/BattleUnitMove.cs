using UnityEngine;

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

    protected override void Update()
    {
        DetectEnemy();

        base.Update();
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
        //Ÿ���� ���� ��� �����Ÿ� �ȿ� ���� �� ������ �̵�
        if (target.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (nav.stoppingDistance != attackRange - 0.1f)
                nav.stoppingDistance = attackRange - 0.1f;
        }
        else if (priorityTarget.gameObject.layer == LayerMask.NameToLayer("Installation"))
        { //Ÿ���� �ǹ��� ��� Ÿ���� ���������� �̵�
            Collider targetCollider = priorityTarget.GetComponent<Collider>();
            //����
            Vector3 tangentPoint = targetCollider.ClosestPoint(transform.position);

            nav.stoppingDistance = 1.0f;
            nav.SetDestination(tangentPoint);
        }
        //�ƴ� ��� ������ �̵�
        else
        {
            if (nav.stoppingDistance != 0f)
                nav.stoppingDistance = 0f;
        }

        nav.SetDestination(target.position);

        if (nav.velocity.sqrMagnitude >= 0.1f //��ã�� �����Ҷ��� ���� �Ÿ��� 0���� �߰ԵǹǷ�, �����̴� �������� üũ
            && nav.remainingDistance <= nav.stoppingDistance + 0.1f)
        {
            ResetTarget();
        }

        //TODO: ��ֹ��� ���� �̵��� �Ұ��� ���°� ���ӵ� ���?
    }
}