using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // [SerializeField]private static BuildingManager building;
    // [SerializeField]private static PlayerData playerData;
    // [SerializeField]private static MapManager map;
    // [SerializeField]private static AchievementManager achieve;
    // [SerializeField]private static MapShadow shadow;

    // public BuildingManager Building { get => building; }
    // public PlayerData P_Data { get => playerData; }
    // public MapManager Map { get => map; }
    // public AchievementManager Achieve { get => achieve; }
    // public MapShadow Shadow { get => shadow; }

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);

    }
    private void Start()
    {
        print(tower_Player[0]);
    }

    public List<Transform> tower_Player = new List<Transform>();
    public List<Transform> unit_Player  = new List<Transform>();      
    
}
