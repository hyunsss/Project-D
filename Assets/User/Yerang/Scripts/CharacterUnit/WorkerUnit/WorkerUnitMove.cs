using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private Animator animator;

    private Transform target;

    private ArrowDrawer arrowDrawer;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        arrowDrawer = GetComponent<ArrowDrawer>();

        state = State.Idle;
    }

    private void Update()
    {
        //Input
        target = arrowDrawer.destination;


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

    private void MoveToTarget()
    {
        nav.SetDestination(target.position);

        if (nav.velocity.sqrMagnitude >= 0.1f //길찾기 시작할때도 남은 거리가 0으로 뜨게되므로, 움직이는 상태인지 체크
            && nav.remainingDistance <= nav.stoppingDistance + 0.1f)
        {
            target = null;

            if (arrowDrawer.destination != null)
                Destroy(arrowDrawer.destination.gameObject);
        }
    }
}
