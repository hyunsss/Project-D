using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBall : Projectile
{
    protected override void OnMove()
    {
        transform.Translate((target.position - transform.position).normalized
            * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.1)
        {
            //변경완료(성민)
            target.GetComponent<Monster>().HitDamage(damage); //TODO: TestEnemy -> Enemy
            Lean.Pool.LeanPool.Despawn(this);
        }
    }
}