using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : ScriptableObject
{
    public string unitName;

    public int maxHp;
    protected int currentHp;
    public int dp;

    public int ap;
    public int attackCycle;

    public float moveSpeed;
    public float attackRange;

    public int price;
}
