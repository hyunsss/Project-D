using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public string unitName;

    public float maxHp;
    protected float currentHp;
    public float dp;

    public float price;

    //public event EventHandler GetDamageEvent;
    private HpBar hpBar;

    protected virtual void Awake()
    {
        Canvas canvas = GetComponentInChildren<Canvas>();
        hpBar = GetComponentInChildren<Canvas>().GetComponentInChildren<HpBar>();
        canvas.gameObject.SetActive(false);
    }

    protected void OnEnable()
    {
        currentHp = maxHp;
        hpBar.SetHpBar(currentHp, maxHp);
    }

    public void GetDamage(float damage)
    {
        currentHp -= damage;
        if(currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }

        hpBar.SetHpBar(currentHp, maxHp);
    }

    private void Die()
    {
        Lean.Pool.LeanPool.Despawn(gameObject);
    }
}
