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
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // ------------- Player Game Object List(DB) -------------------
    public List<Transform>      tower_Player = new List<Transform>();
    public List<Transform>      unit_Player  = new List<Transform>();
    public List<Transform>      scv_Player   = new List<Transform>();

    // ------------ Player Cost Value      (mineral, gas) ------------------------------
    public int tower_HP_Level_UP      = 1;
    public int tower_Damage_Level_UP  = 1;
    public int unit_HP_Level_UP       = 1;
    public int unit_Damage_Level_UP   = 1;
    // ------------- Monster DB-------------------------------------
    public int                  currentMonsterCount = 0;


    [SerializeField]
    private int mineral;
    public int Mineral { get { return mineral; } }


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
