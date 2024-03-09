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

    protected Unit unit; //��...


    protected virtual void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        unit = GetComponent<Unit>();

        state = State.Idle;
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

    protected void Start()
    {
        nav.speed = moveSpeed;
    }

    public abstract void SetPriorityTarget(Transform target);

    protected abstract void ResetTarget();

    protected abstract void MoveToTarget();
}
