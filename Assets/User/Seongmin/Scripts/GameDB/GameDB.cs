using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDB : MonoBehaviour
{
   
    public static GameDB Instance;

    public Dictionary<int, Unit> selectedTable = new Dictionary<int, Unit>();

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
}
