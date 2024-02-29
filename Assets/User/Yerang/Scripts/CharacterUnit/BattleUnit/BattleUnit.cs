using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleUnit : Unit
{
    public float ap;
    public int attackCycle;
    public float attackRange;

    public Transform priorityTarget;
    public Transform target;

    private BattleUnitMove unitMove;
    protected Animator animator;

    [SerializeField]
    protected Transform shotPoint;

    protected bool isAttack = false;

    private void OnEnable()
    {
        unitMove = GetComponent<BattleUnitMove>();
        animator = GetComponentInChildren<Animator>();
        currentHp = maxHp;
    }

    private void Update()
    {
        SetTarget();

        if (target != null && !isAttack 
            && unitMove.state != BattleUnitMove.State.Move)
        {
            isAttack = true;
            Attack();
        }
        else if(isAttack
            && ((target == null) || (unitMove.state == BattleUnitMove.State.Move)))
        {
            isAttack = false;
            EndAttack();
        }
    }
    

    public abstract void Attack();
    public abstract void EndAttack();

    public void SetPriorityTarget(Transform target)
    {
        priorityTarget = target;
    }

    Collider[] attackColliders;
    private void SetTarget()
    {
        //Ÿ���� ��Ȱ��ȭ ����?
        if (target != null && !target.gameObject.activeSelf)
        {
            target = null;
        }

        //�켱���� Ÿ���� ����� ������ priorityTarget�� target���� �����ϰ� ��������
        if (priorityTarget != null) 
        {
            target = priorityTarget;
            return;
        }

        int enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
        attackColliders = Physics.OverlapSphere(transform.position, attackRange, enemyLayerMask);

        float minDis = 999;
        Transform nearTarget = null;

        foreach (Collider collider in attackColliders)
        {
            float dis = Vector3.Distance(collider.transform.position, transform.position);
            if (dis < minDis)
            {
                minDis = dis;
                nearTarget = collider.transform;
            }
        }

        this.target = nearTarget;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
