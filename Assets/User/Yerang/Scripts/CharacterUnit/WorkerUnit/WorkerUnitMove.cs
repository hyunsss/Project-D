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

    private WorkerUnit workerUnit; //��...


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
        else //Ÿ���� �� ����Ʈ�� �ƴҰ�� Ÿ���� ���������� �̵�
        {
            Collider targetCollider = target.GetComponent<Collider>();
            //����
            Vector3 tangentPoint = targetCollider.ClosestPoint(transform.position);

            nav.stoppingDistance = 1.0f;
            nav.SetDestination(tangentPoint);
        }
        
        if (nav.velocity.sqrMagnitude >= 0.1f //��ã�� �����Ҷ��� ���� �Ÿ��� 0���� �߰ԵǹǷ�, �����̴� �������� üũ
            && nav.remainingDistance <= nav.stoppingDistance + 0.1f)
        {
            if (target.gameObject.layer != LayerMask.NameToLayer("GoalPoint"))
                workerUnit.Collocate(target.GetComponent<Installation>());

            ResetTarget();
        }
    }
}
