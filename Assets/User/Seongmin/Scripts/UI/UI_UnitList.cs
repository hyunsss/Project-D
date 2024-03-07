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
    private List<Button> slotList = new List<Button>();
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
            Unit currentUnit = null;

            if (unit.TryGetComponent(out ArcherUnit _archerUnit))
            {
                currentSprite = listImage[0];
                currentUnit = _archerUnit;
            }
            else if(unit.TryGetComponent(out MageUnit _mageUnit))
            {
                currentSprite = listImage[1];
                currentUnit = _mageUnit;
            }
            else if(unit.TryGetComponent(out WarriorUnit _warriorUnit))
            {
                currentSprite = listImage[2];
                currentUnit = _warriorUnit;
            }


            if (currentSprite != null && slotCount < slotList.Count)
            {
                slotList[slotCount].gameObject.SetActive(true);
                slotList[slotCount].transform.Find("Image").GetComponent<Image>().sprite = currentSprite;
                if (slotList[slotCount].gameObject.TryGetComponent(out UI_Slot _slotData))
                {
                    _slotData.slotUnitData = currentUnit;
                }
                slotCount++;
            }
            else
            {
                break;
            }
            
            
        }
    }

    public void SlotReset()
    {
        foreach(var slot in slotList)
        {
            slotList[slotCount].transform.Find("Image").GetComponent<Image>().sprite = null;
            slot.gameObject.SetActive(false);
        }
    }
}
