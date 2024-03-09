using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*[Serializable]
public struct Resource
{
    public int mineral;
    public int gas;
}*/

public class TestGameManager : MonoBehaviour
{
    private static TestGameManager instance;
    public static TestGameManager Instance { get { return instance; } }

    //private Resource ownResource;
    //public Resource OwnResource { get { return ownResource; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    /*public void GainMineral(int mineral)
    {
        this.ownResource.mineral += mineral;
    }

    public void GainGas(int gas)
    {
        this.ownResource.gas += gas;
    }

    public bool UseReSource(Resource requiredResource)
    {
        if (requiredResource.mineral >= ownResource.mineral //재화가 부족하면
            || requiredResource.gas >= ownResource.gas)
        {
            return false;
        }

        ownResource.mineral -= requiredResource.mineral;
        ownResource.gas -= requiredResource.gas;
        return true;
    }*/
}
