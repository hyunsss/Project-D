using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

//�˾Ƽ� ����� Ÿ�� ã�� �̵�
//�������� �̵� ��Ű��
public class UnitMove : MonoBehaviour
{
    public float moveSpeed;

    bool isCommand = false; //����� ���� ��������?
    public enum State
    {
        Idle,
        Move
    }
    public State state;

    public float detectingRange;
    private float attackRange;

    private NavMeshAgent nav;

    private Transform target;

    private void Awake()
    {
        attackRange = GetComponent<UnitAction>().attackRange;
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        /*if (!isCommand) //����� ���� ������ ���� ������ Ÿ����
        {
            DetectEnemy();
        }*/

        switch (state)
        {
            case State.Idle:
                if(target != null)
                {
                    state = State.Move;
                    break;
                }
                if (!isCommand) //����� ���� ������ ���� ������ Ÿ����
                {
                    DetectEnemy();
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

        print(state);
    }

    public void CommandTargeting(Transform target)
    {
        this.target = target;
        isCommand = true;
        //print("��� �Էµ�");
    }

    private void DetectEnemy()
    {
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

        if(minDis > attackRange && minDis < 999)
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
        print("MovetoTarget Enable");
        //Ÿ���� ���� ��� �����Ÿ� �ȿ� ���� �� ������ �̵�
        if (target.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
        {
            if(nav.stoppingDistance != attackRange)
                nav.stoppingDistance = attackRange;
        }
        //�ƴ� ��� ������ �̵�
        else
        {
            if(nav.stoppingDistance != 0)
                nav.stoppingDistance = 0;
        }

        nav.SetDestination(target.position);

        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            target = null;
            if (isCommand)
                isCommand = false;
        }
    }
}
