using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretTowerInfo", menuName = "Scriptable Object Asset/TowerInfo/TurretTowerInfo")]
public class TurretTowerInfo : ScriptableObject
{
    public int maxlevel;

    [Serializable]
    public struct Stat
    {
        public float maxHp;
        public float damage;
        public float attackCycle;
        public float attackRange;
    }
    [SerializeField]
    public Stat[] levelStat;

    public GameObject[] rendererPrefabs;
}
