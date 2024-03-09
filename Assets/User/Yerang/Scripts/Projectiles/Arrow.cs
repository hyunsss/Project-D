using System.Collections;
using UnityEngine;

public class Arrow : Projectile
{
    public float maxHeight;

    private Vector3 startPosition; //ȭ���� �߻�� ��ġ, �������� �׸� �� ���� ��ġ ���� �־��� ��ŭ���� ��� �ϱ� ����
    private Vector3 lastFramePosTemp; //���� �����ӿ� ȭ���� �ִ� ��ġ

    private Transform rendererTransform;
    private TrailRenderer trailRenderer;

    private void Awake()
    {
        rendererTransform = transform.GetChild(0); //0: ������
        trailRenderer = rendererTransform.GetComponent<TrailRenderer>();
        Debug.Log(rendererTransform);
    }

    protected void OnEnable()
    {
        trailRenderer.Clear();
        startPosition = transform.position;
    }

    protected override void OnMove()
    {

        //������Ʈ �̵�
        transform.Translate((target.position - transform.position).normalized
            * speed * Time.deltaTime, Space.World);

        //������ �̵�
        float totalDistance = Vector3.Distance(startPosition, target.position);
        float remainDistance = Vector3.Distance(transform.position, target.position);

        //��ü ������ ���� ������ ��ġ�� ����: 1 - (�����Ÿ� / ��ü�Ÿ�)
        float t = 1f - (remainDistance / totalDistance);

        float rendererPosY = Mathf.Sin(Mathf.Lerp(0, 180, t) * Mathf.Deg2Rad) * maxHeight;

        Vector3 rendererHeight = new Vector3(0, rendererPosY, 0);

        rendererTransform.localPosition = rendererHeight;

        //���� �����ӿ� �ִ� ��ġ(rendererTransform.forward)�� ������ ���ϵ���
        rendererTransform.forward = -(lastFramePosTemp - rendererTransform.position).normalized;

        lastFramePosTemp = rendererTransform.position;


        if (remainDistance <= 0.1) //Ÿ�ٰ��� ���� �Ÿ��� 0.1�����̸� �������� �ְ� ����
        {
            if (target.TryGetComponent<Monster>(out Monster monster)) //TODO: TestEnemy -> Enemy
                monster.HitDamage(damage);
            if (target.TryGetComponent<MonsterTower>(out MonsterTower tower))
                tower.HitDamage(damage);
            Lean.Pool.LeanPool.Despawn(this);
        }


    }

    /*private void OnTriggerEnter(Collider other) //���� ������ �������� �ְ� ����
    { //TODO: TestEnemy -> Enemy
        if (other.gameObject.TryGetComponent<TestEnemy>(out TestEnemy testEnemy))
        {
            testEnemy.GetDamage(damage);
            Lean.Pool.LeanPool.Despawn(this);
        }
    }*/
}
