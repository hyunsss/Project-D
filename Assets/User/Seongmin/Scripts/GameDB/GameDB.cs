using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct Resource
{
    public int mineral;
    public int gas;
    public Resource(int _mineral, int _gas)
    {
        this.mineral = _mineral;
        this.gas = _gas;
    }
}


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
    public Resource tower_HP_Level_UP      = new(10,0);
    public Resource tower_Damage_Level_UP  = new(10,0);
    public Resource unit_HP_Level_UP       = new(10,0);
    public Resource unit_Damage_Level_UP   = new(10,0);
    // ------------- Monster DB-------------------------------------
    public int                  currentMonsterCount = 0;

    [SerializeField]
    private Resource ownResource;
    public Resource OwnResource { get { return ownResource; } }

    
    public void GainMineral(int mineral)
    {
        this.ownResource.mineral += mineral;
    }

    public void GainGas(int gas)
    {
        this.ownResource.gas += gas;
    }

    public bool IsEnoughResource(Resource requiredResource)
    {
        if (requiredResource.mineral > ownResource.mineral //재화가 부족하면
            || requiredResource.gas > ownResource.gas)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool UseReSource(Resource requiredResource)
    {
        if (requiredResource.mineral > ownResource.mineral //재화가 부족하면
            || requiredResource.gas > ownResource.gas)
        {
            //TODO UI 띄우기
            return false;
        }

        ownResource.mineral -= requiredResource.mineral;
        ownResource.gas -= requiredResource.gas;
        return true;
    }
}
