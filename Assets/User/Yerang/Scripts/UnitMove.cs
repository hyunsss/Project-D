using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

//알아서 가까운 타겟 찾아 이동
//수동으로 이동 시키기
public class UnitMove : MonoBehaviour
{
    public float moveSpeed;

    bool isCommand = false; //명령을 받은 상태인지?
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
        /*if (!isCommand) //명령을 수행 중이지 않을 때에만 타겟팅
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
                if (!isCommand) //명령을 수행 중이지 않을 때에만 타겟팅
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
        //print("명령 입력됨");
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
        /*타겟이 사정거리보다 멀리있으면
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
        //타겟이 적일 경우 사정거리 안에 들어올 때 까지만 이동
        if (target.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
        {
            if(nav.stoppingDistance != attackRange)
                nav.stoppingDistance = attackRange;
        }
        //아닐 경우 끝까지 이동
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
