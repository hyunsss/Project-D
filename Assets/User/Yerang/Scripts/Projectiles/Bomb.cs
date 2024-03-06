using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class Bomb : Projectile
{
    public float explosionRange = 6f;

    //public ParticleSystem ExplosionParticle;
    public VisualEffect ExplosionParticle;

    private Vector3 startPosition;
    private Vector3 lastFramePosTemp;
    private Vector3 targetPosition;

    private Transform rendererTransform;

    private bool isAttack;

    private void OnEnable()
    {
        ExplosionParticle = GetComponent<VisualEffect>();
        GetComponentInChildren<MeshRenderer>().enabled = true;
        rendererTransform = transform.GetChild(0); //0: 렌더러

        startPosition = transform.position;
        isAttack = false;
    }

    public override void InitProjctile(float damage, Transform target)
    {
        this.damage = damage;
        this.target = target;
        targetPosition = target.position;
    }

    protected override void OnMove()
    {
        if (isAttack)
        {
            return;
        }

        transform.Translate((targetPosition - transform.position).normalized 
            * speed * Time.deltaTime, Space.World);

        float totalDistance = Vector3.Distance(startPosition, targetPosition);

        float remainDistance = Vector3.Distance(transform.position, targetPosition);

        float t = 1f - (remainDistance / totalDistance);

        float rendererPosY = Mathf.Sin(Mathf.Lerp(0, 180, t) * Mathf.Deg2Rad) * 10; //MaxHeight = 10

        Vector3 rendererHeight = new Vector3(0, rendererPosY, 0);

        rendererTransform.localPosition = rendererHeight;

        rendererTransform.up = (lastFramePosTemp - rendererTransform.position).normalized;
        lastFramePosTemp = rendererTransform.position;

        if (remainDistance <= 0.1)
        {
            GetComponentInChildren<MeshRenderer>().enabled = false;
            ExplosionAttack();
        }
    }

    private void ExplosionAttack()
    {
        isAttack = true;

        ExplosionParticle.Play(); //TODO: 매니저로 부르기

        int layerMask = LayerMask.GetMask("Enemy");
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange, layerMask);
        if (colliders != null)
        {
            for (int i = 0; i < colliders.Length; i++) //TODO: TestEnemy -> Enemy
            {
                //Debug.Log(colliders[i]);
                // 변경완료(성민)
                if (colliders[i] == null) continue;
                colliders[i].gameObject.GetComponent<Monster>().SendMessage("HitDamage", damage, SendMessageOptions.DontRequireReceiver);
            }
        }

        StartCoroutine(RemoveCoroutine());
    }

    private IEnumerator RemoveCoroutine()
    {
        /*while (ExplosionParticle.sto)
        {
            yield return null;
        }*/
        yield return new WaitForSeconds(3f);
        LeanPool.Despawn(gameObject);
    }
}
