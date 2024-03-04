using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

//�˾Ƽ� ����� Ÿ�� ã�� �̵�
//�������� �̵� ��Ű��
public class BattleUnitMove : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move
    }
    public State state;

    public float moveSpeed;
    public float detectingRange;
    private float attackRange;

    private NavMeshAgent nav;
    private Animator animator;

    private UnityEngine.Transform target;
    private UnityEngine.Transform priorityTarget;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        attackRange = GetComponent<BattleUnit>().attackRange;

        state = State.Idle;
    }

    private void Start()
    {
        nav.speed = moveSpeed;
    }

    private void Update()
    {
        DetectEnemy();

        switch (state)
        {
            case State.Idle:
                if (target != null)
                {
                    state = State.Move;
                }
                break;

            case State.Move:
                if (target == null)
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
        UnityEngine.Transform nearTarget = null;

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

    public void SetPriorityTarget(UnityEngine.Transform target) //ArrowDrawer�� ����
    {
        if (priorityTarget != null)
        {
            ResetTarget();
        }

        this.priorityTarget = target;
    }

    private void ResetTarget()
    {
        if (priorityTarget != null)
        {
            Lean.Pool.LeanPool.Despawn(priorityTarget.gameObject);
            priorityTarget = null;
            target = null;
        }

        if (target != null)
        {
            target = null;
        }
    }

    private void MoveToTarget()
    {
        //Ÿ���� ���� ��� �����Ÿ� �ȿ� ���� �� ������ �̵�
        if (target.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (nav.stoppingDistance != attackRange - 0.1f)
                nav.stoppingDistance = attackRange - 0.1f;
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