using System.Collections;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected Transform target;

    protected float damage;
    public float speed;

    public virtual void InitProjctile(float damage, Transform target)
    {
        this.damage = damage;
        this.target = target;
    }

    protected void Update()
    {
        //타겟이 비활성화 상태?
        if (target != null && !target.gameObject.activeSelf)
        {
            target = null;
        }

        if (target == null) //타겟이 없어지면 삭제
        {
            Lean.Pool.LeanPool.Despawn(this);
            return;
        }

        OnMove();
    }

    protected abstract void OnMove();
}