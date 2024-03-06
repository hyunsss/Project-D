using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
        foreach(var unit in GameDB.Instance.selectedTable.Values)
        {
            //if()
            if (TryGetComponent(out ArcherUnit _archerUnit))
            {
              
            }
            else if(TryGetComponent(out MageUnit _mageUnit))
            {
              
            }
            else if(TryGetComponent(out WarriorUnit _warriorUnit))
            {

            }
        }
    }

    private void UnitDataSetting()
    {

    }
}
