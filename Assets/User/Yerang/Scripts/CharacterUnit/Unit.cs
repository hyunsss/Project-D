using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;

    public float maxHp;
    protected float currentHp;
    
    public float dp;

    public float price;

    public void GetDamage(float damage)
    {
        currentHp -= damage;
        if(currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    private void Die()
    {
        Lean.Pool.LeanPool.Despawn(gameObject);
    }
}
