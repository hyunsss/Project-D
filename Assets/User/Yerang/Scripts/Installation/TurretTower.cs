using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretTower : Tower
{
    [SerializeField]
    protected TurretTowerInfo turretTowerInfo;

    protected float ap;
    public float Ap {  get { return ap; } }

    protected float attackCycle;
    public float AttackCycle { get { return attackCycle; } }

    protected float attackRange;
    public float AttackRange { get { return attackRange; } }

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
        detectedEnemies.Clear();
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

        Destroy(transform.GetChild(2).gameObject);
        Instantiate(turretTowerInfo.rendererPrefabs[level - 1], transform);
        transform.GetChild(2).TryGetComponent<Animator>(out animator); //2: Render

        currentHp = maxHp;
        detectingCollider.radius = attackRange;

        StopAllCoroutines();
    }

    public abstract void Attack();

    public abstract void EndAttack();

    //Àû °¨Áö
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
