using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float maxHp;
    private float currentHp;

    public float BuildingTime;
    public float FixedHpPerSec;

    private bool isCompleteBuilt;

    private GameObject BeingBuiltPrefab;

    public void Built()
    {
        Instantiate(BeingBuiltPrefab, transform);
    }

    public void GetDamage(float damage)
    {
        currentHp -= damage;
    }

    public IEnumerator Fixed()
    {
        yield return new WaitForSeconds(1f);

        //¹Ýº¹¹®
        currentHp += FixedHpPerSec;
    }

    public void Destroyed()
    {
        //To-Do: LeanPool
        Destroy(gameObject);
    }
}
