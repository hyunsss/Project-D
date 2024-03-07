using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FieldInfo", menuName = "Scriptable Object Asset/InstallationInfo/FieldInfo")]
public class FieldInfo : ScriptableObject
{
    public int maxlevel;

    public Resource[] price;

    [Serializable]
    public struct Stat
    {
        public float maxHp;
        public int amountPerSec;
    }
    public Stat[] levelStat;
}
