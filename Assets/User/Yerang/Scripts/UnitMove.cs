using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

//알아서 가까운 타겟 찾아 이동
//수동으로 이동 시키기
public class UnitMove : MonoBehaviour
{
    public float moveSpeed;

    public enum State
    {
        Idle,
        CommandMove, //사용자의 명령에 의한 움직임
        AutoMaticChase //자동으로 적을 추적하는 상태
    }
    public State state;

    public float detectingRange;
    private float attackRange;

    private NavMeshAgent nav;

    private Transform target;

    private Vector3 goalPoint;
    //private Vector3 moveDir;

    private void Awake()
    {
        attackRange = GetComponent<UnitAction>().attackRange;
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        DetectTarget();

        switch (state)
        {
            case State.Idle:
                nav.SetDestination(transform.position);
                if (target != null)
                    state = State.AutoMaticChase;
                break;
            case State.CommandMove:

                break;
            case State.AutoMaticChase:
                ChaseTarget();
                break;
            default:
                break;
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void DetectTarget()
    {
        int enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, detectingRange, enemyLayerMask);

        /*print(detectedColliders.Length);
        foreach (Collider collider in detectedColliders)
        {
            print(collider.gameObject);
        }*/

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
        target = nearTarget;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectingRange);
    }

    private void ChaseTarget()
    {
        if (target == null)
        {
            nav.SetDestination(transform.position);
            state = State.Idle;
            return;
        }

        //타겟이 사정거리보다 멀리있으면
        if (Vector3.Distance(target.position, transform.position) > attackRange)
        {
            nav.SetDestination(target.position);
        }
        else
        {
            nav.SetDestination(transform.position);
            state = State.Idle;
        }
    }
}
