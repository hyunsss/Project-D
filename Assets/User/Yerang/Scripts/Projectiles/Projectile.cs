using System.Collections;
using Lean.Pool;
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
        //Ÿ���� ��Ȱ��ȭ ����?
        if (target != null && !target.gameObject.activeSelf)
        {
            target = null;
        }

        if (target == null) //Ÿ���� �������� ����
        {
            LeanPool.Despawn(this);
            return;
        }

        if(target != null) OnMove();
    }

    protected abstract void OnMove();
}