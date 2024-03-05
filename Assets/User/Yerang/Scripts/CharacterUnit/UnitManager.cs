using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private static UnitManager instance;
    public static UnitManager Instance { get { return instance; } }

    private Transform unitParent;
    public Transform UnitParent { get { return unitParent; } }

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

        unitParent = transform;
    }
}
