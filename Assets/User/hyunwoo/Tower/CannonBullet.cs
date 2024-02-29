using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class CannonBullet : TowerBullet
{
    [HideInInspector] public ParticleSystem ExplosionParticle;
    [HideInInspector] public int ExplosionDamage;
    
    private Vector3 targetPos;
    private Vector3 lastFramePosTemp;

    bool isAttack;

    private void Awake() {
        ExplosionDamage = 30;
    }

    protected override void Start() {
        startPosition = transform.position;
    }

    private void OnEnable() {
        GetComponentInChildren<MeshRenderer>().enabled = true;
        isAttack = false;
    }

    protected override void Update() {

        if(target != null) {
            targetPos = target.transform.position;
        }
        
        transform.Translate((targetPos - transform.position).normalized * Speed * Time.deltaTime, Space.World);

        float totalDistance = Vector3.Distance(startPosition, targetPos);

        float remainDistance = Vector3.Distance(transform.position, targetPos);

        float t = 1f - (remainDistance / totalDistance);

        float rendererPosY = Mathf.Sin(Mathf.Lerp(0, 180, t) * Mathf.Deg2Rad) * maxheight;

        Vector3 rendererHeight = new Vector3( 0, rendererPosY, 0);

        rendererTransform.localPosition = rendererHeight;

        rendererTransform.up = (lastFramePosTemp - rendererTransform.position).normalized;
        lastFramePosTemp = rendererTransform.position;

        if(remainDistance < hitDistance && isAttack == false) {
            target.SendMessage("TakeDamage", Damage, SendMessageOptions.DontRequireReceiver);
            GetComponentInChildren<MeshRenderer>().enabled = false;
            ExplosionAttack();
            isAttack = true;
        }

        DestroyObject();
    }

    private void ExplosionAttack() {
        int layerMask = LayerMask.GetMask("Enemy");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 6f, layerMask);
        if(colliders != null) {
            for(int i = 0; i < colliders.Length; i++) {
                Debug.Log(colliders[i]);
                if(colliders[i] == null) continue;
                colliders[i].gameObject.GetComponent<Monster>().SendMessage("TakeDamage", ExplosionDamage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    protected override void DestroyObject() {
        if(ExplosionParticle != null && ExplosionParticle.isPlaying == false) {
            ExplosionParticle = null;
            LeanPool.Despawn(gameObject);
        }
    }

    

}
