using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBall : Projectile
{
    protected override void OnMove()
    {
        transform.Translate((target.position - transform.position).normalized
            * speed * Time.deltaTime, Space.World);
    }
}