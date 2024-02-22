using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

//알아서 가까운 타겟 찾아 이동
//수동으로 이동 시키기
public class UnitMove : MonoBehaviour
{
    public float moveSpeed;

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
    private Transform priorityTarget;

    private ArrowDrawer arrowDrawer;

    private void Awake()
    {
        attackRange = GetComponent<UnitAction>().attackRange;
        nav = GetComponent<NavMeshAgent>();
        arrowDrawer = GetComponent<ArrowDrawer>();

        state = State.Idle;
    }

    private void Update()
    {
        //Input
        priorityTarget = arrowDrawer.destination;


        if (priorityTarget != null) 
        {
            target = priorityTarget;
        }
        else //명령을 수행 중이지 않을 때에만 타겟팅
        {
            DetectEnemy();
        }

        switch (state)
        {
            case State.Idle:
                if(target != null)
                {
                    state = State.Move;
                }
                break;

            case State.Move:
                if(target == null)
                {
                    state = State.Idle;
                    break;
                }
                MoveToTarget();
                break;
            default:
                break;
        }
    }

    private void DetectEnemy()
    {
        if (priorityTarget != null)
        {
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

        if (minDis > attackRange && minDis < 999)
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
        //타겟이 적일 경우 사정거리 안에 들어올 때 까지만 이동
        if (target.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
        {
            if(nav.stoppingDistance != attackRange - 0.1f)
                nav.stoppingDistance = attackRange - 0.1f;
        }
        //아닐 경우 끝까지 이동
        else
        {
            if(nav.stoppingDistance != 0f)
                nav.stoppingDistance = 0f;
        }

        nav.SetDestination(target.position);

        if (nav.remainingDistance <= nav.stoppingDistance + 0.1f)
        {
            target = null;
            priorityTarget = null;
            arrowDrawer.destination = null;
            state = State.Idle;
        }
    }
}
