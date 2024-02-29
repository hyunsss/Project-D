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

    protected Vector3 startPosition; //화살이 발사된 위치, 포물선을 그릴 때 시작 위치 기준 멀어진 만큼으로 계산 하기 위해
    protected Vector3 lastFramePosTemp; //직전 프레임에 화살이 있던 위치

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
        //타겟이 비활성화 상태?
        if(target != null && !target.gameObject.activeSelf)
        {
            target = null;
        }

        if (target == null) //타겟이 없어지면 삭제
        {
            Lean.Pool.LeanPool.Despawn(this);
            return;
        }

        //오브젝트 이동
        transform.Translate((target.position - transform.position).normalized
            * speed * Time.deltaTime, Space.World);

        //렌더러 이동
        float totalDistance = Vector3.Distance(startPosition, target.position);
        float remainDistance = Vector3.Distance(transform.position, target.position);

        //전체 길이중 현재 도달한 위치의 비율: 1 - (남은거리 / 전체거리)
        float t = 1f - (remainDistance / totalDistance);

        float rendererPosY = Mathf.Sin(Mathf.Lerp(0, 180, t) * Mathf.Deg2Rad) * 0.8f;

        Vector3 rendererHeight = new Vector3(0, rendererPosY, 0);

        rendererTransform.localPosition = rendererHeight;

        //이전 프레임에 있던 위치(rendererTransform.forward)로 꼬리가 향하도록
        rendererTransform.forward = (lastFramePosTemp - rendererTransform.position).normalized;

        lastFramePosTemp = rendererTransform.position;
    }

    IEnumerator RemoveCoroutine() //생명주기가 다하면 삭제
    {
        yield return new WaitForSeconds(lifetime);
        Lean.Pool.LeanPool.Despawn(this);
        yield break;
    }

    private void OnTriggerEnter(Collider other) //적에 닿으면 삭제
    { //TODO: TestEnemy -> Enemy
        if (other.gameObject.TryGetComponent<TestEnemy>(out TestEnemy testEnemy))
        {
            testEnemy.GetDamage(damage);
            Lean.Pool.LeanPool.Despawn(this);
        }
    }
}
