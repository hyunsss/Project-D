using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    Vector3 movePos;
    public GameObject goalPointPrefab;
    Transform targetObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (GameDB.Instance.unitlist.Count > 0)
            {
                //가상 평면에 레이와 마우스 클릭했을 때 쏜 레이가 맞았다면 그 아래 함수를 실행합니다.
                if (MapManager.Instance.plane.Raycast(ray, out float enter))
                {
                    movePos = ray.GetPoint(enter);
                    targetObject = LeanPool.Spawn(goalPointPrefab, movePos, Quaternion.identity).transform;
                }


                foreach (Unit unit in GameDB.Instance.unitlist)
                {
                    UnitMove moveCompo = unit.GetComponent<UnitMove>();
                    moveCompo.SetPriorityTarget(targetObject);
                }
            }
        }
    }
}
