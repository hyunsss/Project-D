using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public float maxHp;
    private float currentHp;

    public float speed;

    private void Awake()
    {
        currentHp = maxHp;
    }

    public void GetDamage(float damage)
    {
        currentHp -= damage;
    }
}
