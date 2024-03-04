using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class WorkerUnitMove : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move
    }
    public State state;

    public float moveSpeed;

    private NavMeshAgent nav;
    //private Animator animator;

    private GameObject prevTarget;
    private UnityEngine.Transform target;

    private WorkerUnit workerUnit; //음...


    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        //animator = GetComponentInChildren<Animator>();

        workerUnit = GetComponent<WorkerUnit>();

        state = State.Idle;
    }

    private void Start()
    {
        nav.speed = moveSpeed;
    }

    private void Update()
    {
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

        //animator.SetInteger("moveState", (int)state);
    }

    public void SetTarget(UnityEngine.Transform target)
    {
        if (target != null)
        {
            ResetTarget();
        }

        this.target = target;
    }

    private void ResetTarget()
    {
        if (target != null)
        {
            if (target.gameObject.layer == LayerMask.NameToLayer("GoalPoint"))
            {
                Lean.Pool.LeanPool.Despawn(target.gameObject);
                target = null;
            }

            else
                target = null;
        }
    }

    private void MoveToTarget()
    {
        if (target.gameObject.layer == LayerMask.NameToLayer("GoalPoint"))
        {
            nav.stoppingDistance = 0f;
            nav.SetDestination(target.position);

        }
        else //타겟이 골 포인트가 아닐경우 타겟의 인접점까지 이동
        {
            Collider targetCollider = target.GetComponent<Collider>();
            //접점
            Vector3 tangentPoint = targetCollider.ClosestPoint(transform.position);

            nav.stoppingDistance = 1.0f;
            nav.SetDestination(tangentPoint);
        }
        
        if (nav.velocity.sqrMagnitude >= 0.1f //길찾기 시작할때도 남은 거리가 0으로 뜨게되므로, 움직이는 상태인지 체크
            && nav.remainingDistance <= nav.stoppingDistance + 0.1f)
        {
            if (target.gameObject.layer != LayerMask.NameToLayer("GoalPoint"))
                workerUnit.Collocate(target.GetComponent<Installation>());

            ResetTarget();
        }
    }
}
