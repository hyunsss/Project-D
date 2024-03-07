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


    public void UnitListDraw()
    {
        int slotCount = 0;
        Sprite currentSprite = null;
        foreach(var unit in GameDB.Instance.selectedTable.Values)
        {
            //if()
            if (TryGetComponent(out ArcherUnit _archerUnit))
            {
                currentSprite = listImage[0];
            }
            else if(TryGetComponent(out MageUnit _mageUnit))
            {
                currentSprite = listImage[1];
            }
            else if(TryGetComponent(out WarriorUnit _warriorUnit))
            {
                currentSprite = listImage[2];
            }
            slotList[slotCount].gameObject.SetActive(true);
            slotList[slotCount].sprite = currentSprite;
            slotCount++;
            
        }
    }

    private void UnitDataSetting()
    {

    }
}
