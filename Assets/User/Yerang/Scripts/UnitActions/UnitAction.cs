using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//2022.3.19
public abstract class UnitAction : MonoBehaviour
{
    public string unitName;

    public int maxHp;
    protected int currentHp;
    public int dp;

    public int ap;
    public int attackCycle;
    public float attackRange;

    public Transform target;
    public Transform shotPoint;

    public int price; //

    private void Awake()
    {
        currentHp = maxHp;
    }

    private void Update()
    {
        SetTarget();
    }
    

    public abstract void Attack();

    public void GetDamage(int damage)
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

    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    Collider[] attackColliders;
    private void SetTarget()
    {
        int enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
        attackColliders = Physics.OverlapSphere(transform.position, attackRange, enemyLayerMask);

        //print(attackColliders.Length);
        /*foreach (Collider collider in attackColliders)
        {
            print(collider.gameObject);
        }*/

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
        //print(nearTarget);
        this.target = nearTarget;
        //print(target);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
