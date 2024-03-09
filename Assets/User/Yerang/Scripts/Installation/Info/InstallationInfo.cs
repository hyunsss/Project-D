using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InstallationInfo : ScriptableObject
{
    public string installationName;
    public int maxlevel;

    public int[] price;

    public float[] maxHp;
}
