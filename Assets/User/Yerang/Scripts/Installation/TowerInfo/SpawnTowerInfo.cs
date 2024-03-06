using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnTowerInfo", menuName = "Scriptable Object Asset/TowerInfo/SpawnTowerInfo")]
public class SpawnTowerInfo : ScriptableObject
{
    public int maxlevel;

    public float[] maxHp;

    public Resource[] price;

    public GameObject[] rendererPrefabs;
}