using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;
    private Transform target;

    public float speed;
    public float lifetime;

    protected Vector3 startPosition; //ȭ���� �߻�� ��ġ, �������� �׸� �� ���� ��ġ ���� �־��� ��ŭ���� ��� �ϱ� ����
    protected Vector3 lastFramePosTemp; //���� �����ӿ� ȭ���� �ִ� ��ġ

    public Transform rendererTransform;

    private void OnEnable()
    {
        startPosition = transform.position;
        StartCoroutine(RemoveCoroutine());
    }

    

    public void InitProjctile(float damage, Transform target)
    {
        this.damage = damage;
        this.target = target;
    }

    private void Update()
    {
        OnMove();
    }

    private void OnMove()
    {
        //Ÿ���� ��Ȱ��ȭ ����?
        if(target != null && !target.gameObject.activeSelf)
        {
            target = null;
        }

        if (target == null) //Ÿ���� �������� ����
        {
            Lean.Pool.LeanPool.Despawn(this);
            return;
        }

        //������Ʈ �̵�
        transform.Translate((target.position - transform.position).normalized
            * speed * Time.deltaTime, Space.World);

        //������ �̵�
        float totalDistance = Vector3.Distance(startPosition, target.position);
        float remainDistance = Vector3.Distance(transform.position, target.position);

        //��ü ������ ���� ������ ��ġ�� ����: 1 - (�����Ÿ� / ��ü�Ÿ�)
        float t = 1f - (remainDistance / totalDistance);

        float rendererPosY = Mathf.Sin(Mathf.Lerp(0, 180, t) * Mathf.Deg2Rad) * 0.8f;

        Vector3 rendererHeight = new Vector3(0, rendererPosY, 0);

        rendererTransform.localPosition = rendererHeight;

        //���� �����ӿ� �ִ� ��ġ(rendererTransform.forward)�� ������ ���ϵ���
        rendererTransform.forward = (lastFramePosTemp - rendererTransform.position).normalized;

        lastFramePosTemp = rendererTransform.position;
    }

    IEnumerator RemoveCoroutine() //�����ֱⰡ ���ϸ� ����
    {
        yield return new WaitForSeconds(lifetime);
        Lean.Pool.LeanPool.Despawn(this);
        yield break;
    }

    private void OnTriggerEnter(Collider other) //���� ������ ����
    { //TODO: TestEnemy -> Enemy
        if (other.gameObject.TryGetComponent<TestEnemy>(out TestEnemy testEnemy))
        {
            testEnemy.GetDamage(damage);
            Lean.Pool.LeanPool.Despawn(this);
        }
    }
}
