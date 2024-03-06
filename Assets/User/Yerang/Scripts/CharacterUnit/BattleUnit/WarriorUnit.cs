using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorUnit : BattleUnit
{
    [SerializeField]
    private GameObject attackPrefab;
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
            //타겟을 바라보도록
            transform.rotation = Quaternion.LookRotation(target.position - transform.position).normalized;

            //TODO: Visual Effect Manager?
            var attack = Lean.Pool.LeanPool.Spawn(attackPrefab,
                shotPoint.position, shotPoint.rotation, transform.GetChild(0)); //0: ShotPoint

            animator.SetTrigger("attack");

            yield return new WaitForSeconds(attackCycle);

            Destroy(attack.gameObject);
        }
    }
}
