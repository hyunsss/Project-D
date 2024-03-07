using System.Collections;
using UnityEngine;

public class Arrow : Projectile
{
    public float maxHeight;

    private Vector3 startPosition; //화살이 발사된 위치, 포물선을 그릴 때 시작 위치 기준 멀어진 만큼으로 계산 하기 위해
    private Vector3 lastFramePosTemp; //직전 프레임에 화살이 있던 위치

    private Transform rendererTransform;
    private TrailRenderer trailRenderer;

    private void Awake()
    {
        rendererTransform = transform.GetChild(0); //0: 렌더러
        trailRenderer = rendererTransform.GetComponent<TrailRenderer>();
    }

    protected void OnEnable()
    {
        trailRenderer.Clear();
        startPosition = transform.position;
    }

    protected override void OnMove()
    {
        //오브젝트 이동
        transform.Translate((target.position - transform.position).normalized
            * speed * Time.deltaTime, Space.World);

        //렌더러 이동
        float totalDistance = Vector3.Distance(startPosition, target.position);
        float remainDistance = Vector3.Distance(transform.position, target.position);

        //전체 길이중 현재 도달한 위치의 비율: 1 - (남은거리 / 전체거리)
        float t = 1f - (remainDistance / totalDistance);

        float rendererPosY = Mathf.Sin(Mathf.Lerp(0, 180, t) * Mathf.Deg2Rad) * maxHeight;

        Vector3 rendererHeight = new Vector3(0, rendererPosY, 0);

        rendererTransform.localPosition = rendererHeight;

        //이전 프레임에 있던 위치(rendererTransform.forward)로 꼬리가 향하도록
        rendererTransform.forward = -(lastFramePosTemp - rendererTransform.position).normalized;

        lastFramePosTemp = rendererTransform.position;


        if (remainDistance <= 0.1) //타겟과의 남은 거리가 0.1이하이면 데미지를 주고 삭제
        {
            target.GetComponent<Monster>().HitDamage(damage); //  변경 완료(성민) 
            Lean.Pool.LeanPool.Despawn(this);
        }
    }

    /*private void OnTriggerEnter(Collider other) //적에 닿으면 데미지를 주고 삭제
    { //TODO: TestEnemy -> Enemy
        if (other.gameObject.TryGetComponent<TestEnemy>(out TestEnemy testEnemy))
        {
            testEnemy.GetDamage(damage);
            Lean.Pool.LeanPool.Despawn(this);
        }
    }*/
}
