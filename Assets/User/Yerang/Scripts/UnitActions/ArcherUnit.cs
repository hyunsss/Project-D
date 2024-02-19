using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherUnit : BattleUnit
{
    [SerializeField]
    private Projectile attackPrefab;

    Coroutine attackCoroutine;

    private void Update()
    {
        if(target != null && attackCoroutine == null)
        {
            Attack();
        }
    }

    public override void Attack()
    {
        attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    public void EndAttack()
    {
        StopCoroutine(attackCoroutine);
        attackCoroutine = null;
    }

    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            var attack = Instantiate(attackPrefab, shotPoint.position, shotPoint.rotation);
            attack.InitProjctile(ap, target);

            yield return new WaitForSeconds(attackCycle);
        }
    }
}
