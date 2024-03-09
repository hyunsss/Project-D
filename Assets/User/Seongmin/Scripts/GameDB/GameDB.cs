using System;
using System.Collections;
using System.Collections.Generic;
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
    public List<Transform>      tower_Player = new List<Transform>();
    public List<Transform>      unit_Player = new List<Transform>();
    public List<Transform>      scv_Player = new List<Transform>();
    public int                  monsterCount = 0;

    public int mineral;

    public void GainMineral(int mineral)
    {
        this.mineral += mineral;
    }

    public bool IsEnoughResource(int requiredmineral)
    {
        if (requiredmineral > mineral) //재화가 부족하면
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
        if (requiredmineral > mineral) //재화가 부족하면
        {
            //TODO UI 띄우기
            return false;
        }

        mineral -= requiredmineral;
        return true;
    }
}
