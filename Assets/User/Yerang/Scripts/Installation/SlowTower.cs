using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTower : TurretTower
{
    public float slowPer;
    public GameObject detectingArea;
    public override void Attack()
    {
        detectingArea.SetActive(true);
    }

    private void Stun(TestEnemy enemy)
    {
        //콜라이더 생성
        enemy.speed = enemy.speed * (1 - slowPer / 100);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(TryGetComponent<TestEnemy>(out TestEnemy enemy))
        {
            Stun(enemy);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
