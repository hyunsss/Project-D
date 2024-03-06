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
        detectingCollider = transform.GetChild(0).GetComponent<SphereCollider>(); //0: DetectingArea
        shotPoint = transform.GetChild(1); //1: ShotPoint
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SetInfo();
        detectedEnemies.Clear();
    }

    public virtual void SetInfo()
    {
        //���� ����
        this.maxHp = towerInfo.levelStat[level - 1].maxHp;
        this.ap = towerInfo.levelStat[level - 1].ap;
        this.attackCycle = towerInfo.levelStat[level - 1].attackCycle;
        this.attackRange = towerInfo.levelStat[level - 1].attackRange;

        currentHp = maxHp;
        detectingCollider.radius = attackRange;

        //������ ����
        Destroy(transform.GetChild(2).gameObject);
        Instantiate(towerInfo.rendererPrefabs[level - 1], transform);
        transform.GetChild(2).TryGetComponent<Animator>(out animator); //2: Render

        StopAllCoroutines();
    }

    public abstract void Attack();

    public abstract void EndAttack();

    //�� ����
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
