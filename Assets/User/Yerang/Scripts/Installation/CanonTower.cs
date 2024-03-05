using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanonTower : TurretTower
{
    private TestEnemy target;

    [SerializeField]
    private Projectile attackPrefab;
    protected Coroutine attackCoroutine = null;

    private void Update()
    {
        SetTarget();
    }

    //<공격>
    public override void Attack()
    {
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AttackCoroutine());
        }
    }

    public override void EndAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            if(animator != null)
                animator.SetTrigger("Attack");

            var attack = Lean.Pool.LeanPool.Spawn(attackPrefab,
                    shotPoint.position, shotPoint.rotation, ProjectileManager.Instance.ProjectileParent);
            attack.InitProjctile(ap, target.transform);

            yield return new WaitForSeconds(attackCycle);
        }
    }

    //<타겟팅>
    protected void SetTarget()
    {
        if (detectedEnemies.Count <= 0) //감지된 적이 없으면 빠져나감
        {
            target = null;
            return;
        }

        //가장 가까운 적 찾기
        float minDisSqr = 999f;

        foreach (var detectedEnemie in detectedEnemies)
        {
            float DisSqr 
                = Vector3.SqrMagnitude(detectedEnemie.transform.position - transform.position);

            if(DisSqr < minDisSqr)
            {
                minDisSqr = DisSqr;
                target = detectedEnemie;
            }
        }

        if (target != null)
        {
            Attack();
        }
        else
        {
            EndAttack();
        }
    }
}
