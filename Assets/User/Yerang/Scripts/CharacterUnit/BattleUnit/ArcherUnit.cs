using System.Collections;
using UnityEngine;

public class ArcherUnit : BattleUnit
{
    [SerializeField]
    private Projectile attackPrefab;
    protected Coroutine attackCoroutine = null;

    public override void Attack()
    {
        if(attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AttackCoroutine());
        }
    }

    public override void EndAttack()
    {
        if(attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            //타겟을 바라보도록
            transform.rotation = Quaternion.LookRotation(target.position - transform.position).normalized;

            var attack = Lean.Pool.LeanPool.Spawn(attackPrefab, 
                shotPoint.position, shotPoint.rotation, ProjectileManager.Instance.ProjectileParent);
            attack.InitProjctile(ap, target);

            animator.SetTrigger("attack");

            yield return new WaitForSeconds(attackCycle);
        }
    }
}
