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
    private int  HPlevelUP   = 1;
    private int  DamgelevelUP = 1;
    void Start()
    {
        towerHpUp = transform.Find("TowerHpUp").GetComponent<Button>();
        towerDamageUp = transform.Find("TowerDamageUp").GetComponent<Button>();
        unitDamageUp = transform.Find("UnitDamageUp").GetComponent<Button>();
        unitHpUp = transform.Find("UnitHpUp").GetComponent<Button>();

        towerHpUp.onClick.AddListener(() => TowerHpUp());
        towerDamageUp.onClick.AddListener(() => TowerDamageUp());
        unitDamageUp.onClick.AddListener(() => UnitDamageUp());
        unitHpUp.onClick.AddListener(() => UnitHpUp());
    }

    private void TowerHpUp()
    {
        foreach(var tower in GameDB.Instance.tower_Player)
        {
           if(tower.gameObject.TryGetComponent<Installation>(out Installation _tower))
           {
                _tower.maxHp += HPlevelUP;
                _tower.CurrentHp += HPlevelUP;
           }
        }
        
       // GameDB.Instance.UseReSource()
    }
    private void TowerDamageUp()
    {
        foreach (var tower in GameDB.Instance.tower_Player)
        {
            if (tower.gameObject.TryGetComponent<TowerAttack>(out TowerAttack _tower))
            {
                _tower.Damage += DamgelevelUP;
            }
        }
    }
    private void UnitHpUp()
    {
        foreach(var  unit in GameDB.Instance.unit_Player)
        {
            if(unit.gameObject.TryGetComponent<Unit>(out Unit _unit))
            {
                _unit.maxHp +=     HPlevelUP;
                _unit.CurrentHP += HPlevelUP;
            }
        }
    }
    private void UnitDamageUp()
    {
        foreach(var unit in GameDB.Instance.unit_Player)
        {
            if(unit.gameObject.TryGetComponent<BattleUnit>(out BattleUnit _unit))
            {
                _unit.ap += DamgelevelUP;
            }
        }
    }
}
