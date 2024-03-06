using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AcherTower : TurretTower
{
    private List<TestEnemy> targets = new List<TestEnemy>();

    [SerializeField]
    private Projectile attackPrefab;
    protected Coroutine attackCoroutine = null;

    private void Update()
    {
        SetTarget();
    }

    //<공격>
    public override void Attack()
    {
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AttackCoroutine());
        }
    }

    public override void EndAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            foreach (var target in targets)
            {
                var attack = Lean.Pool.LeanPool.Spawn(attackPrefab,
                    shotPoint.position, shotPoint.rotation, ProjectileManager.Instance.ProjectileParent);
                attack.InitProjctile(ap, target.transform);
            }

            yield return new WaitForSeconds(attackCycle);
        }
    }

    //<타겟팅>
    protected void SetTarget()
    {
        if(detectedEnemies.Count <= 0) //감지된 적이 없으면 빠져나감
        {
            EndAttack();
            return;
        }

        //감지된 적들을 거리순으로 정렬
        var SortAboutDis = detectedEnemies.OrderBy(
            x => Vector3.Distance(transform.position, x.transform.position)).ToList();

        //레벨만큼 타겟을 지정
        targets.Clear();

        int flag;
        if (SortAboutDis.Count < level)
            flag = SortAboutDis.Count;
        else
            flag = level;

        for (int i = 0; i < flag; i++)
        {
            if (SortAboutDis[i] != null)
                targets.Add(SortAboutDis[i]);
        }

        if(targets.Count > 0)
        {
            Attack();
        }
        else
        {
            EndAttack();
        }
    }
}
