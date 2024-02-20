using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private MonsterData monsterData;
    public MonsterData MonsterData { set { monsterData = value; } }

    
    public enum State
    {
        chase,
        attack,
        die
    }
    public State state;

    private void Start()
    {
        state = State.chase;
       // StartCoroutine(ChangeState(state));
    }

    //IEnumerator ChangeState(State state)
    //{
        
    //}

}
