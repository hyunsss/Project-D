using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FieldInfo", menuName = "Scriptable Object Asset/TowerInfo/FieldInfo")]

public class FieldInfo : ScriptableObject
{
    public int maxlevel;

    [Serializable]
    public struct Stat
    {
        public float maxHp;
        public float amountPerSec;
    }
    public Stat[] levelStat;

    public Resource[] price;
}
