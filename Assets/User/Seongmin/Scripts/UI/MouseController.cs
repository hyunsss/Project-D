using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MouseController : MonoBehaviour
{
    public  GameObject      Selected_image;

    public  Transform       unitCam;

    private GameObject      target = null;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<Monster>())
                {
                    var selectedUnit = hit.collider.GetComponent<Monster>();
                    target = selectedUnit.gameObject;
                    print("몬스터가 선택 되었습니다.");
                    ClickSelected(target);
                }
                else 
                {
                    print("몬스터가 선택 해제 되었습니다.");
                    DeSelected();
                }
            }
        }
        if(target != null)
        {
            Selected_image.transform.position = target.transform.position+Vector3.up*10f;
        }   
    }

    private void ClickSelected(GameObject _target)
    {
        unitCam.transform.SetParent(_target.transform);
        unitCam.transform.parent = _target.transform;
        unitCam.localPosition = new Vector3(0, 3f, 2.5f);
        unitCam.localRotation = Quaternion.Euler(35f, 180f, 0);
        unitCam.transform.localScale = new Vector3(1,1,1);  
        Selected_image.SetActive(true);
    }

    private void DeSelected()
    {
        Selected_image.SetActive(false);
    }
}
