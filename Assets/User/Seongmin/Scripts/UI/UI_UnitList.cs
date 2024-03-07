using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

        foreach(var unit in GameDB.Instance.unitlist)
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
            else if(slotCount>=slotList.Count)
            {
                break;
            }
            
            
        }
    }

    private void UnitDataSetting()
    {

    }
}
