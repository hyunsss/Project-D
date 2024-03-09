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
    private int HPlevelUP = 1;
    private int DamgelevelUP = 1;
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
        if (GameDB.Instance.UseReSource(GameDB.Instance.cost_Tower_HP_Level_UP))
        {
            foreach (var tower in GameDB.Instance.tower_Player)
            {
                if (tower.gameObject.TryGetComponent<Installation>(out Installation _tower))
                {
                    _tower.maxHp += HPlevelUP;
                    _tower.CurrentHp += HPlevelUP;
                }
            }
            GameDB.Instance.value_Tower_HP_Level_UP++;
        }
        else
        {
            UI_PanelManager.Instance.NoMoneyMessage();
        }
    }

    private void TowerDamageUp()
    {
        if (GameDB.Instance.UseReSource(GameDB.Instance.cost_Tower_Damage_Level_UP))
        {
            foreach (var tower in GameDB.Instance.tower_Player)
            {
                if (tower.gameObject.TryGetComponent<TowerAttack>(out TowerAttack _tower))
                {
                    _tower.Damage += DamgelevelUP;
                }
            }
            GameDB.Instance.value_Tower_Damgae_Level_UP++;
        }
        else
        {
            UI_PanelManager.Instance.NoMoneyMessage();
        }
    }
    private void UnitHpUp()
    {
        if (GameDB.Instance.UseReSource(GameDB.Instance.cost_Unit_HP_Level_UP))
        {
            foreach (var unit in GameDB.Instance.unit_Player)
            {
                if (unit.gameObject.TryGetComponent<Unit>(out Unit _unit))
                {
                    _unit.maxHp += HPlevelUP;
                    _unit.CurrentHP += HPlevelUP;
                }
            }
            GameDB.Instance.value_Unit_HP_Level_UP++;
        }
        else
        {
            UI_PanelManager.Instance.NoMoneyMessage();
        }
    }
    private void UnitDamageUp()
    {
        if (GameDB.Instance.UseReSource(GameDB.Instance.cost_Unit_Damage_Level_UP))
        {
            foreach (var unit in GameDB.Instance.unit_Player)
            {
                if (unit.gameObject.TryGetComponent<BattleUnit>(out BattleUnit _unit))
                {
                    _unit.ap += DamgelevelUP;
                }
            }
            GameDB.Instance.value_Unit_Damage_Level_UP++;
        }
        else
        {
            UI_PanelManager.Instance.NoMoneyMessage();
        }
    }
}
