using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretTower : Tower
{
    protected float ap;
    protected float attackCycle;
    protected float attackRange;

    protected Transform shotPoint;

    protected SphereCollider detectingCollider;
    protected List<TestEnemy> detectedEnemies = new List<TestEnemy>();

    [SerializeField]
    protected TurretTowerInfo turretTowerInfo;
    

    protected override void Awake()
    {
        base.Awake();
        detectingCollider = transform.GetChild(0).GetComponent<SphereCollider>(); //0: DetectingArea
        shotPoint = transform.GetChild(1); //1: ShotPoint
    }

    public override void SetInfo()
    {
        if(level >= turretTowerInfo.maxlevel)
        {
            isCanUpgrade = false;
        }

        this.maxHp = turretTowerInfo.levelStat[level - 1].maxHp;
        this.ap = turretTowerInfo.levelStat[level - 1].maxHp;
        this.attackCycle = turretTowerInfo.levelStat[level - 1].attackCycle;
        this.attackRange = turretTowerInfo.levelStat[level - 1].attackRange;

        currentHp = maxHp;
        detectingCollider.radius = attackRange;

        StopAllCoroutines();
    }

    public abstract void Attack();

    public abstract void EndAttack();

}
