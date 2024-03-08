using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Resource
{
    public int mineral;
    public int gas;
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
    public List<Transform>      tower_Player = new List<Transform>();
    public List<Transform>      unit_Player = new List<Transform>();
    public List<Transform>      scv_Player = new List<Transform>();
    public int                  monsterCount = 0;

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
        if (requiredResource.mineral >= ownResource.mineral //재화가 부족하면
            || requiredResource.gas >= ownResource.gas)
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
            return false;
        }

        ownResource.mineral -= requiredResource.mineral;
        ownResource.gas -= requiredResource.gas;
        return true;
    }
}
