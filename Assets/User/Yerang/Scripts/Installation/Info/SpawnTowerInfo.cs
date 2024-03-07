using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnTowerInfo", menuName = "Scriptable Object Asset/TowerInfo/SpawnTowerInfo")]
public class SpawnTowerInfo : ScriptableObject
{
    public int maxlevel;

    [Serializable]
    public struct Stat
    {
        public float maxHp;
        public float iteration;
    }
    public Stat[] levelStat;

    public Resource[] price;

    public GameObject[] rendererPrefabs;
}