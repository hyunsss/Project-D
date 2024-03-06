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

    //<����>
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

    //<Ÿ����>
    protected void SetTarget()
    {
        if(detectedEnemies.Count <= 0) //������ ���� ������ ��������
        {
            EndAttack();
            return;
        }

        //������ ������ �Ÿ������� ����
        var SortAboutDis = detectedEnemies.OrderBy(
            x => Vector3.Distance(transform.position, x.transform.position)).ToList();

        //������ŭ Ÿ���� ����
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
