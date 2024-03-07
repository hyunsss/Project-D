using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTower : TurretTower
{
    public float slowPer;
    public GameObject detectingArea;

    protected override void OnEnable()
    {
        base.OnEnable();
        Attack();
    }

    public override void SetTower()
    {
        //½ºÅÈ ¼³Á¤
        this.maxHp = towerInfo.levelStat[level - 1].maxHp;
        this.attackRange = towerInfo.levelStat[level - 1].attackRange;

        currentHp = maxHp;
        hpBar.SetHpBar(currentHp, maxHp);

        detectingCollider.radius = attackRange;
        detectingArea.transform.localScale = new Vector3(attackRange * 2, 0.1f, attackRange * 2);

        //·»´õ·¯ ¼³Á¤
        SetRender();

        StopAllCoroutines();
    }

    public override void Attack()
    {
        detectingArea.SetActive(true);
    }

    public override void EndAttack()
    {
        detectingArea.SetActive(false);
    }

    private void Stun(TestEnemy enemy)
    {
        enemy.speed = enemy.speed * (1 - slowPer / 100);
    }

    private void Destun(TestEnemy enemy)
    {
        enemy.speed = enemy.speed / (1 - slowPer / 100);
    }

    private new void OnTriggerEnter(Collider other)
    { //TODO: TestEnemy -> Enemy
        if(TryGetComponent<TestEnemy>(out TestEnemy enemy))
        {
            Stun(enemy);
        }
    }

    private new void OnTriggerExit(Collider other)
    {
        if (TryGetComponent<TestEnemy>(out TestEnemy enemy))
        {
            Destun(enemy);
        }
    }
}
