using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slot : MonoBehaviour
{

   
    public Unit slotUnitData;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickChildButton);
    }
    private void OnClickChildButton()
    {
        //--------------- INFO Panel Setting-----------

        if (slotUnitData.TryGetComponent(out BattleUnit _battleUnit))
        {
            UI_PanelManager.Instance.BattleUnitPanel_OPEN();
            UI_PanelManager.Instance.gameObjectINFO.BattleUnitSetINFO(_battleUnit);
        }
        if (slotUnitData.TryGetComponent(out WorkerUnit _workerUnit))
        {
            UI_PanelManager.Instance.WorkerUnitPanel_OPEN();
            UI_PanelManager.Instance.gameObjectINFO.WorkerUnitSetINFO(_workerUnit);
        }
    }
}
