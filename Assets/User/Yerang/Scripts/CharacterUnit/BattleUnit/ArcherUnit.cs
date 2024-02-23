using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherUnit : BattleUnit
{
    [SerializeField]
    private Projectile attackPrefab;

    public override void Attack()
    {
        attackCoroutine = StartCoroutine(AttackCoroutine());
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
            animator.SetTrigger("attack");

            var attack = Instantiate(attackPrefab, shotPoint.position, shotPoint.rotation, 
                transform.GetChild(0)); //0: ShotPoint
            attack.InitProjctile(ap, target);
            yield return new WaitForSeconds(attackCycle);
        }
    }
}
