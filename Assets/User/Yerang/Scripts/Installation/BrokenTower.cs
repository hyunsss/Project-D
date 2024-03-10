using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenTower : MonoBehaviour
{
    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Broken()
    {
        animator.SetTrigger("Broken");
        Lean.Pool.LeanPool.Despawn(gameObject, 1f);
    }
}
