using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnTowerInfo", menuName = "Scriptable Object Asset/InstallationInfo/SpawnTowerInfo")]
public class SpawnTowerInfo : ScriptableObject
{
    public int maxlevel;

    public Resource[] price;

    [Serializable]
    public struct Stat
    {
        public float maxHp;
        public float iteration;
    }
    public Stat[] levelStat;

    public GameObject[] rendererPrefabs;
}