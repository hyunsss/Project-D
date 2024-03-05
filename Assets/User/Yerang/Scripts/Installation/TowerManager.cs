using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private static TowerManager instance;
    public static TowerManager Instance { get { return instance; } }

    private Transform towerParent;
    public Transform TowerParent { get { return towerParent; } }

    /*[Serializable]
    public struct TurretTowerSet
    {
        public TurretTower turretTower;
        public TurretTowerInfo turretTowerInfo;
    }
    public TurretTowerSet[] turretTowerSets;

    private Dictionary<string, TurretTowerInfo> turretTowerDic = new Dictionary<string, TurretTowerInfo>();*/

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

        towerParent = transform;

        //SetDictionary();
    }

    /*private void SetDictionary()
    {
        foreach (TurretTowerSet turretTowerSet in turretTowerSets)
        {
            turretTowerDic.Add(turretTowerSet.turretTower.name, turretTowerSet.turretTowerInfo);
        }
    }*/

    /*public void BuildTower()
    {
    }*/

    public void UpgradeTower(TurretTower turretTower)
    {
        //재화사용

        if (turretTower.IsCanUpgrade)
        {
            turretTower.level++;
            turretTower.SetInfo();
        }
    }
}
