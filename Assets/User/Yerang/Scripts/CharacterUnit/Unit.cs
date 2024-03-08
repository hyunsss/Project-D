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
    public float CurrentHP { get { return currentHp; } }
    
    public float dp;

    public float price;

    //public event EventHandler GetDamageEvent;
    private Canvas canvas;
    private HpBar hpBar;

    protected virtual void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        hpBar = GetComponentInChildren<Canvas>().GetComponentInChildren<HpBar>();
    }

    protected void OnEnable()
    {
        currentHp = maxHp;
        hpBar.SetHpBar(currentHp, maxHp);
        canvas.gameObject.SetActive(false);
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
