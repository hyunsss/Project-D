using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpawnTowerInfo;

public abstract class TurretTower : Tower
{
    public TurretTowerInfo towerInfo;

    protected float ap;
    protected float attackCycle;
    protected float attackRange;

    protected Transform shotPoint;

    protected SphereCollider detectingCollider;
    protected List<TestEnemy> detectedEnemies = new List<TestEnemy>();

    protected Animator animator;

    protected override void Awake()
    {
        base.Awake();
        detectingCollider = transform.GetChild(0).GetComponent<SphereCollider>(); //0: DetectingArea
        shotPoint = transform.GetChild(1); //1: ShotPoint
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SetInfo();
        detectedEnemies.Clear();
    }

    public override void SetInfo()
    {
        //스탯 설정
        this.maxHp = towerInfo.levelStat[level - 1].maxHp;
        this.ap = towerInfo.levelStat[level - 1].ap;
        this.attackCycle = towerInfo.levelStat[level - 1].attackCycle;
        this.attackRange = towerInfo.levelStat[level - 1].attackRange;

        currentHp = maxHp;
        detectingCollider.radius = attackRange;

        //렌더러 설정
        Destroy(transform.GetChild(3).gameObject); //3: Render
        Instantiate(towerInfo.rendererPrefabs[level - 1], transform);
        transform.GetChild(3).TryGetComponent<Animator>(out animator);

        StopAllCoroutines();

        hpBar.SetHpBar(currentHp, maxHp);
    }

    public abstract void Attack();

    public abstract void EndAttack();

    //적 감지
    protected void OnTriggerEnter(Collider other)
    { //TODO: TestEnemy -> Enemy
        if (other.TryGetComponent<TestEnemy>(out TestEnemy enemy))
        {
            detectedEnemies.Add(enemy);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<TestEnemy>(out TestEnemy enemy))
        {
            detectedEnemies.Remove(enemy);
        }
    }

}
