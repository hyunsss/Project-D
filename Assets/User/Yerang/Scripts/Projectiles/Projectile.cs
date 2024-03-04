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

    protected IEnumerator RemoveCoroutine() //생명주기가 다하면 삭제
    {
        yield return new WaitForSeconds(lifetime);
        Lean.Pool.LeanPool.Despawn(this);
        yield break;
    }

    protected void OnTriggerEnter(Collider other) //적에 닿으면 삭제
    { //TODO: TestEnemy -> Enemy
        if (other.gameObject.TryGetComponent<TestEnemy>(out TestEnemy testEnemy))
        {
            testEnemy.GetDamage(damage);
            Lean.Pool.LeanPool.Despawn(this);
        }
    }
}