using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameDB : MonoBehaviour
{

    public static GameDB Instance;


    public Dictionary<int, Unit> selectedTable = new Dictionary<int, Unit>();
    public List<Unit> unitlist = new List<Unit>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ------------- Player InGame Object List(DB) -------------------
    public List<Transform> tower_Player = new List<Transform>();
    public List<Transform> unit_Player = new List<Transform>();
    public List<Transform> scv_Player = new List<Transform>();
    // ------------ Player Money ----------------------------------
    [SerializeField]
    private int mineral;
    public int Mineral { get { return mineral; } }
    // ------------ Player Cost Value :  mineral---------------------
    public int cost_Tower_HP_Level_UP = 1;
    public int cost_Tower_Damage_Level_UP = 1;
    public int cost_Unit_HP_Level_UP = 1;
    public int cost_Unit_Damage_Level_UP = 1;
    // ------------ Player LevelUP Data --------------------

    public int value_Tower_HP_Level_UP = 0;
    public int value_Tower_Damgae_Level_UP = 0;
    public int value_Unit_HP_Level_UP = 0;
    public int value_Unit_Damage_Level_UP = 0;

    //-------------Player Tower Cost Value : mineral-----------------
    public int cost_Archer_Tower = 50;
    public int cost_Canon_Tower = 50;
    public int cost_Slow_Tower = 50;
    public int cost_Spawn_Tower = 50;
    // ------------- Monster DB-------------------------------------
    public int currentMonsterCount = 0;



    public void GainMineral(int mineral)
    {
        this.mineral += mineral;
    }

    public bool IsEnoughResource(int requiredmineral)
    {
        if (requiredmineral > mineral)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool UseReSource(int requiredmineral)
    {
        if (requiredmineral > mineral)
        {
            return false;
        }

        mineral -= requiredmineral;
        return true;
    }
}
