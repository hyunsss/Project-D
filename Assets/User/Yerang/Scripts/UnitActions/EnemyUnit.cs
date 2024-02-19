using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : UnitAction
{
    public override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(attackCycle);
        }
    }
}
