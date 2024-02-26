using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTowerKeeper : Monster
{
    protected override  void Start()
    {
        state = State.chase;
        aStar.speed = monsterData.MonsterSpeed;
        currentHp = monsterData.MonsterHp;
        StartCoroutine(ChangeState());
    }
}
