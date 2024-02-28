using UnityEngine;

public class UnitData : ScriptableObject
{
    public string unitName;

    public float maxHp;
    protected float currentHp;
    public float dp;

    public float ap;
    public int attackCycle;

    public float moveSpeed;
    public float attackRange;

    public int price;
}
