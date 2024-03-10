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
    }

    protected void OnEnable()
    {
        trailRenderer.Clear();
        startPosition = transform.position;
    }

    protected override void OnMove()
    {

        //트랜스폼 이동
        transform.Translate((target.position - transform.position).normalized
            * speed * Time.deltaTime, Space.World);

        //랜더러 이동
        float totalDistance = Vector3.Distance(startPosition, target.position);
        float remainDistance = Vector3.Distance(transform.position, target.position);

        //전체 거리 중 남은 거리의 비율: 1 - (남은 거리 / 전체 거리)
        float t = 1f - (remainDistance / totalDistance);

        float rendererPosY = Mathf.Sin(Mathf.Lerp(0, 180, t) * Mathf.Deg2Rad) * maxHeight;

        Vector3 rendererHeight = new Vector3(0, rendererPosY, 0);

        rendererTransform.localPosition = rendererHeight;

        //화살의 꼬리부분이 이전 위치를 향하도록
        rendererTransform.forward = -(lastFramePosTemp - rendererTransform.position).normalized;

        lastFramePosTemp = rendererTransform.position;


        if (remainDistance <= 0.1) //타겟과의 거리가 0.1이하이면 데미지를 주고 삭제
        {
            if (target.TryGetComponent<Monster>(out Monster monster)) //TODO: TestEnemy -> Enemy
                monster.HitDamage(damage);
            if (target.TryGetComponent<MonsterTower>(out MonsterTower tower))
                tower.HitDamage(damage);

            Lean.Pool.LeanPool.Despawn(gameObject);
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
