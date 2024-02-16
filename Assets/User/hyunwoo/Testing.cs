using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    Grid grid;

    public Plane plane;
    float enter;
    void Start()
    {
        grid = new Grid(10, 10, 10f);
        //y = 0인 평면 가상 플레인을 생성합니다.
        plane = new Plane(Vector3.up, Vector3.zero);
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            //게임 화면상에서 레이를 발사합니다.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //가상 평면에 레이와 마우스 클릭했을 때 쏜 레이가 맞았다면 그 아래 함수를 실행합니다.
            if(plane.Raycast(ray, out enter)) {
                //레이가 만난 지점에서 해당 포지션을 가져옵니다. 
                Vector3 hitPoint = ray.GetPoint(enter);
                grid.SetValue(hitPoint, 60);
            }
        }
    }
    // castplane
}
