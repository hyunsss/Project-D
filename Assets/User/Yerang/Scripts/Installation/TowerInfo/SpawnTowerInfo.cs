using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTowerInfo : MonoBehaviour
{
    [CreateAssetMenu(fileName = "SpawnTowerInfo", menuName = "Scriptable Object Asset/TowerInfo/SpawnTowerInfo")]
    public class TurretTowerInfo : ScriptableObject
    {
        public int maxlevel;

        public Resource[] price;

        public GameObject[] rendererPrefabs;
    }
}
