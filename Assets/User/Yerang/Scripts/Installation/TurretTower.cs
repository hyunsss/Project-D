using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretTower : Tower
{
    public float ap;
    public float attackCycle;
    public float attackRange;
    public abstract void Attack();
}
