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
                    ClickSelected(target);
                    UI_PanelManager.Instance.MonsterINFOPanel_OPEN();
                    UI_PanelManager.Instance.gameObjectINFO.MonsterSetINFO(selectedUnit);
                }
                // ----Player Unit selected-----
                else if( hit.collider.GetComponent<BattleUnit>())
                {
                    var selectedUnit = hit.collider.GetComponent<BattleUnit>();
                    target = selectedUnit.gameObject;
                    ClickSelected(target);
                    UI_PanelManager.Instance.BattleUnitPanel_OPEN();
                    UI_PanelManager.Instance.gameObjectINFO.BattleUnitSetINFO(selectedUnit);
                }
                else if(hit.collider.GetComponent<WorkerUnitMove>())
                {
                    var selectedUnit = hit.collider.GetComponent<WorkerUnitMove>();
                    target = selectedUnit.gameObject;
                    ClickSelected(target);
                    //TODO
                    UI_PanelManager.Instance.WorkerUnitPanel_OPEN();
                }
                else if (hit.collider.GetComponent<TurretTower>())
                {
                    var selectedUnit = hit.collider.GetComponent<TurretTower>();
                    target = selectedUnit.gameObject;
                    ClickSelected(target);
                    UI_PanelManager.Instance.TowerBuildPanel_OPEN();
                }
                else
                { 
                    DeSelected();
                    target = null;
                    unitCam.transform.position = Vector3.zero;
                    UI_PanelManager.Instance.PanelReSet();
                    UI_PanelManager.Instance.unitListPanel.SlotReset();
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
        if (_target == null)
        {
            unitCam.transform.position= Vector3.zero;
        }
        else
        {
            unitCam.transform.SetParent(_target.transform);
            unitCam.transform.parent = _target.transform;
            unitCam.localPosition = new Vector3(0, 3f, 2.5f);
            unitCam.localRotation = Quaternion.Euler(35f, 180f, 0);
            unitCam.transform.localScale = new Vector3(1, 1, 1);
            Selected_image.SetActive(true);
        }
    }

    private void DeSelected()
    {
        Selected_image.SetActive(false);
    }
}
