using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//2022.3.19
public abstract class BattleUnit : MonoBehaviour
{
    public string unitName;

    public float maxHp;
    protected float currentHp;
    public float dp;

    public float ap;
    public int attackCycle;
    public float attackRange;

    public Transform priorityTarget;
    public Transform target;

    private BattleUnitMove unitMove;
    protected Animator animator;

    [SerializeField]
    protected Transform shotPoint;
    protected Coroutine attackCoroutine = null;

    private void Awake()
    {
        unitMove = GetComponent<BattleUnitMove>();
        animator = GetComponentInChildren<Animator>();
        currentHp = maxHp;
    }

    private void Update()
    {
        SetTarget();

        if (target != null && attackCoroutine == null 
            && unitMove.state != BattleUnitMove.State.Move)
        {
            transform.rotation = Quaternion.LookRotation(target.position - transform.position).normalized;
            Attack();
        }
        else if(attackCoroutine != null
            && ((target == null) || (unitMove.state == BattleUnitMove.State.Move)))
        {
            EndAttack();
        }
    }
    

    public abstract void Attack();
    public abstract void EndAttack();

    public void GetDamage(float damage)
    {
        currentHp -= damage;

        if (currentHp < 0)
        {
            currentHp = 0;
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void SetPriorityTarget(Transform target)
    {
        priorityTarget = target;
    }

    Collider[] attackColliders;
    private void SetTarget()
    {
        if (priorityTarget != null) //우선으로 타겟할 대상이 있으면 priorityTarget을 target으로 지정하고 빠져나감
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
