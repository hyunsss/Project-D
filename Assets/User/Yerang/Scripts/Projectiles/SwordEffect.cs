using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEffect : Projectile
{
    protected override void OnMove()
    {
        transform.Translate((target.position - transform.position).normalized
            * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.1)
        {
            if (target.TryGetComponent<Monster>(out Monster monster)) //TODO: TestEnemy -> Enemy
                monster.HitDamage(damage);
            Lean.Pool.LeanPool.Despawn(this);
        }
    }
}
