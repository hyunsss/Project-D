using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

//�˾Ƽ� ����� Ÿ�� ã�� �̵�
//�������� �̵� ��Ű��
public class UnitMove : MonoBehaviour
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

    private Transform target;
    private Transform priorityTarget;

    private ArrowDrawer arrowDrawer;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        attackRange = GetComponent<UnitAction>().attackRange;
        arrowDrawer = GetComponent<ArrowDrawer>();

        state = State.Idle;
    }

    private void Update()
    {
        //Input
        priorityTarget = arrowDrawer.destination;


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

    private void MoveToTarget()
    {
        /*Ÿ���� �����Ÿ����� �ָ�������
        if (Vector3.Distance(target.position, transform.position) > attackRange)
        {
            nav.SetDestination(target.position);
        }
        else
        {
            nav.SetDestination(transform.position);
            state = State.Idle;
        }*/
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
            target = null;

            if (priorityTarget != null)
                Destroy(priorityTarget.gameObject);
        }
    }
}