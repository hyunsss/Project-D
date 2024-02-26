using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.VFX;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;
    public Transform shotpoint;
    public enum VFXDir { Explosion }
    
    public List<GameObject> visualEffects = new List<GameObject>();

    void Awake() {
        Instance = this;
    }

    public void VFXPlay(Transform trans, VFXDir key) {

        GameObject effectObject = visualEffects[(int)key];

        LeanPool.Spawn(effectObject, shotpoint.position, Quaternion.identity, null);
        VisualEffect effect = effectObject.GetComponent<VisualEffect>();

        effect.Play();
    }

}
