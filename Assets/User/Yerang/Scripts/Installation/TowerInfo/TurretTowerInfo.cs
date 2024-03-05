using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretTowerInfo", menuName = "Scriptable Object Asset/TowerInfo/TurretTowerInfo")]
public class TurretTowerInfo : ScriptableObject
{
    public string towerName;
    public int maxlevel;

    [Serializable]
    public struct Stat
    {
        public float maxHp;
        public float ap;
        public float attackCycle;
        public float attackRange;
    }
    public Stat[] levelStat;

    public Resource[] price;

    public GameObject[] rendererPrefabs;
}
