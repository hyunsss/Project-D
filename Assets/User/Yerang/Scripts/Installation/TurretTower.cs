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
    protected List<Monster> detectedEnemies = new List<Monster>();

    protected Animator animator;

    protected override void Awake()
    {
        base.Awake();
        detectingCollider = transform.Find("DetectingArea").GetComponent<SphereCollider>();
        shotPoint = transform.Find("ShotPoint");
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        detectedEnemies.Clear();
    }

    public override void SetTower()
    {
        //스탯 설정
        this.maxHp = towerInfo.levelStat[level - 1].maxHp;
        this.ap = towerInfo.levelStat[level - 1].ap;
        this.attackCycle = towerInfo.levelStat[level - 1].attackCycle;
        this.attackRange = towerInfo.levelStat[level - 1].attackRange;

        currentHp = maxHp;
        hpBar.SetHpBar(currentHp, maxHp);

        detectingCollider.radius = attackRange;

        //렌더러 설정
        SetRender();

        StopAllCoroutines();
    }

    protected void SetRender()
    {
        Transform renderParent = transform.Find("Render");
        Destroy(renderParent.GetChild(0).gameObject);
        Instantiate(towerInfo.rendererPrefabs[level - 1], renderParent);
        renderParent.GetChild(0).TryGetComponent<Animator>(out animator);
    }

    public abstract void Attack();

    public abstract void EndAttack();

    //적 감지
    protected void OnTriggerEnter(Collider other)
    { //TODO: TestEnemy -> Enemy
        if (other.TryGetComponent<Monster>(out Monster enemy))
        {
            detectedEnemies.Add(enemy);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Monster>(out Monster enemy))
        {
            detectedEnemies.Remove(enemy);
        }
    }

}
