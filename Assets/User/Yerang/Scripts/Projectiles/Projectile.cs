using System.Collections;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected float damage;
    protected Transform target;

    public float speed;
    public float lifetime;

    protected virtual void OnEnable()
    {
        StartCoroutine(RemoveCoroutine());
    }

    public void InitProjctile(float damage, Transform target)
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
            Lean.Pool.LeanPool.Despawn(this);
            return;
        }

        OnMove();
    }

    protected abstract void OnMove();

    protected IEnumerator RemoveCoroutine() //�����ֱⰡ ���ϸ� ����
    {
        yield return new WaitForSeconds(lifetime);
        Lean.Pool.LeanPool.Despawn(this);
        yield break;
    }

    protected void OnTriggerEnter(Collider other) //���� ������ ����
    { //TODO: TestEnemy -> Enemy
        if (other.gameObject.TryGetComponent<TestEnemy>(out TestEnemy testEnemy))
        {
            testEnemy.GetDamage(damage);
            Lean.Pool.LeanPool.Despawn(this);
        }
    }
}