using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class UI_UnitList : MonoBehaviour
{
    [Header("SlotList")]
    [SerializeField]
    private List<Image> slotList = new List<Image>();
    [Header("Images")]
    [SerializeField]
    private List<Sprite> listImage = new List<Sprite>();

    private int slotCount = 0;
    public void UnitListDraw()
    {
        SlotReset();
        slotCount = 0;
        foreach (var unit in GameDB.Instance.unitlist)
        {
            Sprite currentSprite = null;

            if (unit.TryGetComponent(out ArcherUnit _archerUnit))
            {
                currentSprite = listImage[0];
            }
            else if(unit.TryGetComponent(out MageUnit _mageUnit))
            {
                currentSprite = listImage[1];
            }
            else if(unit.TryGetComponent(out WarriorUnit _warriorUnit))
            {
                currentSprite = listImage[2];
            }


            if (currentSprite != null && slotCount < slotList.Count)
            {
                slotList[slotCount].gameObject.SetActive(true);
                slotList[slotCount].sprite = currentSprite;
                slotCount++;
            }
            else
            {
                break;
            }
            
            
        }
    }
    //TODO
   /* private void UnitDataSetting()
    {
        //--------------- Check WorkUnit OR BattleUnit -----------

        if (unit.TryGetComponent(out BattleUnit _battleUnit))
        {
            UI_PanelManager.Instance.BattleUnitPanel_OPEN();
            UI_PanelManager.Instance.gameObjectINFO.BattleUnitSetINFO(_battleUnit);
        }
        if (unit.TryGetComponent(out WorkerUnit _workerUnit))
        {
            UI_PanelManager.Instance.WorkerUnitPanel_OPEN();
            UI_PanelManager.Instance.gameObjectINFO.WorkerUnitSetINFO(_workerUnit);
        }
    }*/
    private void SlotReset()
    {
        foreach(var slot in slotList)
        {
            slot.sprite = null;
            slot.gameObject.SetActive(false);
        }
    }
}
