using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    //public TowerType towerType;

    [HideInInspector] public UnityEngine.Transform target;
    [HideInInspector] public float Speed;
    [HideInInspector] public int Damage;
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public ParticleSystem currentParticle;
    public float maxheight;

    public float hitDistance = 0.2f;
    public UnityEngine.Transform rendererTransform;
    private Vector3 lastFramePosTemp;

    private TrailRenderer trail;

    private void Awake() {
        trail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable() {
        trail.Clear();
    }

    protected virtual void Start() {
        startPosition = transform.position;
    }

    protected virtual void Update() {

        if(target == null) {
            DestroyObject();
            return;
        }
        
        transform.Translate((target.position - transform.position).normalized * Speed * Time.deltaTime, Space.World);

        float totalDistance = Vector3.Distance(startPosition, target.position);

        float remainDistance = Vector3.Distance(transform.position, target.position);

        float t = 1f - (remainDistance / totalDistance);

        float rendererPosY = Mathf.Sin(Mathf.Lerp(0, 180, t) * Mathf.Deg2Rad) * maxheight;

        Vector3 rendererHeight = new Vector3( 0, rendererPosY, 0);

        rendererTransform.localPosition = rendererHeight;

        rendererTransform.up = (lastFramePosTemp - rendererTransform.position).normalized;
        lastFramePosTemp = rendererTransform.position;

        if(remainDistance < hitDistance) {
            target.SendMessage("TakeDamage", Damage, SendMessageOptions.DontRequireReceiver);
            DestroyObject();
        }
    
    }

    protected virtual void DestroyObject() {
        LeanPool.Despawn(gameObject);
    }

}
