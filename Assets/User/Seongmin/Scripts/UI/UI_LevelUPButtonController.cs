using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LevelUPButtonController : MonoBehaviour
{
    private Button towerHpUp;
    private Button towerDamageUp;
    private Button unitDamageUp;
    private Button unitHpUp;
    void Start()
    {
        towerHpUp = transform.Find("TowerHpUp").GetComponent<Button>();
        towerDamageUp = transform.Find("TowerDamageUp").GetComponent<Button>();
        unitDamageUp = transform.Find("UnitDamageUp").GetComponent<Button>();
        unitHpUp = transform.Find("UnitHpUp").GetComponent<Button>();

       // towerHpUp.onClick.AddListener(() => { })
    }

    private void TowerHpUp()
    {
        foreach(var tower in GameDB.Instance.tower_Player)
        {
         //   tower.gameObject.TryGetComponent<Tower>().
        }

    }
}
