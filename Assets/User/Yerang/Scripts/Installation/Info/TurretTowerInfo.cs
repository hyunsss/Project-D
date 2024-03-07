using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretTowerInfo", menuName = "Scriptable Object Asset/InstallationInfo/TurretTowerInfo")]
public class TurretTowerInfo : ScriptableObject
{
    public int maxlevel;

    public Resource[] price;

    [Serializable]
    public struct Stat
    {
        public float maxHp;
        public float ap;
        public float attackCycle;
        public float attackRange;
    }
    public Stat[] levelStat;

    public GameObject[] rendererPrefabs;
}
