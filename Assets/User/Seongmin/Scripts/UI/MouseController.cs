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
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    // ----Monster selected -------
                    if (hit.collider.GetComponent<Monster>())
                    {
                        var selectedUnit = hit.collider.GetComponent<Monster>();
                        target = selectedUnit.gameObject;
                        ClickSelected(target);
                        UI_PanelManager.Instance.MonsterINFOPanel_OPEN();
                        UI_PanelManager.Instance.gameObjectINFO.MonsterSetINFO(selectedUnit);
                    }
                    // ----Player Unit selected-----
                    else if (hit.collider.GetComponent<BattleUnit>())
                    {
                        var selectedUnit = hit.collider.GetComponent<BattleUnit>();
                        target = selectedUnit.gameObject;
                        ClickSelected(target);
                        UI_PanelManager.Instance.BattleUnitPanel_OPEN();
                        UI_PanelManager.Instance.gameObjectINFO.BattleUnitSetINFO(selectedUnit);
                    }
                    else if (hit.collider.GetComponent<WorkerUnit>())
                    {
                        var selectedUnit = hit.collider.GetComponent<WorkerUnit>();
                        target = selectedUnit.gameObject;
                        ClickSelected(target);
                        UI_PanelManager.Instance.WorkerUnitPanel_OPEN();
                        UI_PanelManager.Instance.gameObjectINFO.WorkerUnitSetINFO(selectedUnit);
                    }
                    // ------Player Tower selected----------------
                    else if (hit.collider.GetComponent<TurretTower>())
                    {
                        var selectedUnit = hit.collider.GetComponent<TurretTower>();
                        target = selectedUnit.gameObject.transform.Find("Render").gameObject;
                        ClickSelected(target);
                        UI_PanelManager.Instance.TowerBuildPanel_OPEN();
                        UI_PanelManager.Instance.gameObjectINFO.PlayerTowerSetINFO(selectedUnit);
                    }
                    else if (hit.collider.GetComponent<SpawnTower>())
                    {
                        var selectedUnit = hit.collider.GetComponent<SpawnTower>();
                        target = selectedUnit.gameObject.transform.Find("Render").gameObject;
                        ClickSelected(target);
                        UI_PanelManager.Instance.SpawnTowerPanel_OPEN();
                        UI_PanelManager.Instance.ui_SpawnTowerUnitListPanel.GetComponent<SpawnUnitListPanel>().currentSpawnTower = selectedUnit;
                        UI_PanelManager.Instance.ui_SpawnTowerPanel.TryGetComponent(out UI_SpawnTowerPanel panel);
                        panel.TowerPrefab = selectedUnit;
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
        }
        if(target != null)
        {
            Selected_image.transform.position = target.transform.position+Vector3.up*10f;
        }   
    }
    public void ClickSelected(GameObject _target)
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
