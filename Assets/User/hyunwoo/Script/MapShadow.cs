using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapShadow : MonoBehaviour
{
    public Transform skytransform;
    // Update is called once per frame

    private void Start()
    {
        StartCoroutine(ShadowCoroutine());
    }

    /// <summary>
    /// 파라미터를 기준으로 원형의 피직스오버랩을 하여 모든 콜라이더를 추출해내고 콜라이더들을 순회하며 해당 material의 alpha값을 변경합니다.
    /// </summary>
    /// <param name="center_trans">레이에 맞은 콜라이더의 트랜스폼 파라미터를 할당 받습니다.</param>
    private void UnitLight(Transform center_trans, float radius)
    {
        Collider[] shadow_colls = Physics.OverlapSphere(center_trans.position, 40f, 1 << 29);

        for (int i = 0; i < shadow_colls.Length; i++)
        {
            MeshRenderer mesh = shadow_colls[i].GetComponent<MeshRenderer>();
            //Distance를 0 ~ 1의 값으로 정규화하여 거리에 따라 alpha값이 다르게 나오도록 합니다
            float alphaRadio = Vector3.Distance(center_trans.position, shadow_colls[i].transform.position) / 30f;
            Color originColor = mesh.material.color;
            //0.4이하의 값들은 그냥 alpha값을 0이 되도록 -0.4f 만큼의 수치를 빼고
            float Tempalpha = Mathf.Min(originColor.a, alphaRadio - 0.4f);
            //그 값이 0이하로 떨어지지 않게 Mathf.Max로 막아줍니다.
            float alpha = Mathf.Max(0, Tempalpha);
            mesh.material.color = new Color(originColor.r, originColor.g, originColor.b, alpha);
        }

    }

    IEnumerator ShadowCoroutine()
    {
        while (true)
        {
            //모든 유닛 정보를 가지고 있는 리스트를 순회합니다
            foreach (Transform unit in GameDB.Instance.unit_Player)
            {
                Vector3 direction = (unit.position - skytransform.position).normalized;
                Debug.DrawLine(skytransform.position, unit.position, Color.red);
                //카메라 포지션에서 유닛의 방향으로 레이를 발사하고 레이에 맞은 콜라이더는 다음 함수를 실행합니다.
                if (Physics.Raycast(skytransform.position, direction, out RaycastHit rayInfo, Mathf.Infinity, 1 << 29))
                {
                    UnitLight(rayInfo.collider.transform, 30f);
                }
            }

            foreach (Transform unit in GameDB.Instance.tower_Player)
            {
                Vector3 direction = (unit.position - skytransform.position).normalized;
                Debug.DrawLine(skytransform.position, unit.position, Color.red);
                //카메라 포지션에서 유닛의 방향으로 레이를 발사하고 레이에 맞은 콜라이더는 다음 함수를 실행합니다.
                if (Physics.Raycast(skytransform.position, direction, out RaycastHit rayInfo, Mathf.Infinity, 1 << 29))
                {
                    UnitLight(rayInfo.collider.transform, 50f);
                }
            }
            yield return new WaitForSeconds(1f);
        }

    }
}
