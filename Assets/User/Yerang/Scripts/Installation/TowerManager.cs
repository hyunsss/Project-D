using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private static TowerManager instance;
    public static TowerManager Instance { get { return instance; } }

    //public List<TowerBeingBuilt> towerBeingBuilts = new List<TowerBeingBuilt>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    /*public void BuildTower()
    {
    }*/

    public void UpgradeTower(TurretTower turretTower)
    {
        if (turretTower.IsCanUpgrade)
        {
            turretTower.level++;
            turretTower.SetInfo();
        }
    }
}
