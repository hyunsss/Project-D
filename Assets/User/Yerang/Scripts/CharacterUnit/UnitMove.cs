using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class UnitMove : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move
    }
    public State state;

    public float moveSpeed;

    protected NavMeshAgent nav;
    protected Animator animator;

    protected Transform priorityTarget;

    protected Unit unit; //À½...


    protected virtual void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        unit = GetComponent<Unit>();

        state = State.Idle;
    }

    protected void Start()
    {
        nav.speed = moveSpeed;
    }

    protected virtual void Update()
    {
        switch (state)
        {
            case State.Idle:
                if (priorityTarget != null)
                {
                    state = State.Move;
                }
                break;

            case State.Move:
                if (priorityTarget == null)
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

    public abstract void SetPriorityTarget(Transform target);

    protected abstract void ResetTarget();

    protected abstract void MoveToTarget();
}
