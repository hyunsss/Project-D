using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.VFX;

public class DespawnEffect : MonoBehaviour
{
    private VisualEffect vfx;
    public float duration;

    private void Awake() {
        vfx = GetComponent<VisualEffect>();
    }

    private void OnEnable() {
        vfx.Play();
        Invoke("Despawn", duration);
    }

    void Despawn() {
        LeanPool.Despawn(this);
    }
    
}
