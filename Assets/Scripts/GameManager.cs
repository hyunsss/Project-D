using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
