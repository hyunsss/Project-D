using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorUnit : BattleUnit
{
    [SerializeField]
    private Projectile attackPrefab;
    protected Coroutine attackCoroutine = null;    

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

    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            //Ÿ���� �ٶ󺸵���
            transform.rotation = Quaternion.LookRotation(target.position - transform.position).normalized;

            //TODO: Visual Effect Manager
            var attack = Lean.Pool.LeanPool.Spawn(attackPrefab,
                shotPoint.position, shotPoint.rotation, ProjectileManager.Instance.ProjectileParent);
            attack.InitProjctile(ap, target);

            animator.SetTrigger("attack");

            yield return new WaitForSeconds(attackCycle);
        }
    }
}