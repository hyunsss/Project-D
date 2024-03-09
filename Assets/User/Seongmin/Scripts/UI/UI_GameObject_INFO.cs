using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UI_GameObject_INFO : MonoBehaviour
{
    public TextMeshProUGUI damage               = null;
    public TextMeshProUGUI HP                   = null;
    public TextMeshProUGUI speed_OR_price       = null;
    public TextMeshProUGUI objectName           = null;

    public void MonsterSetINFO(Monster monster)
    {
        damage.text         = monster.MonsterData.MonsterDamage.ToString();
        HP.text             = monster.MonsterData.MonsterHp.ToString();
        speed_OR_price.text = monster.MonsterData.MonsterSpeed.ToString();
        objectName.text     = monster.MonsterData.MonsterName.ToString();
    }
    public void BattleUnitSetINFO(BattleUnit _battleUnit)
    {
        damage.text         = _battleUnit.ap.ToString();
        HP.text             = _battleUnit.CurrentHP.ToString();
        speed_OR_price.text = _battleUnit.price.ToString();
        objectName.text     = _battleUnit.name.ToString();
    }

    public void PlayerTowerSetINFO(TurretTower _turretTower)
    {   
        damage.text                 = _turretTower.AP.ToString();
        HP.text                     = _turretTower.CurrentHp.ToString();
        speed_OR_price.text         = _turretTower.towerInfo.price.ToString();
        objectName.text             = _turretTower.name.ToString();
    }
    public void WorkerUnitSetINFO(WorkerUnit _workerUnit)
    {
        damage.text             = 0.ToString();
        HP.text                 = _workerUnit.CurrentHP.ToString();
        speed_OR_price.text     = _workerUnit.price.ToString();
        objectName.text         = _workerUnit.name.ToString();
    }
}
