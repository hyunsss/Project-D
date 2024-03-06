using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class TowerManager : MonoBehaviour
{
    private static TowerManager instance;
    public static TowerManager Instance { get { return instance; } }

    private Transform towerParent;
    public Transform TowerParent { get { return towerParent; } }

    /*[SerializeField]
    private TurretTowerInfo[] turretTowerInfos;

    private Dictionary<string, TurretTowerInfo> turretTowerDic = new Dictionary<string, TurretTowerInfo>();*/

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        towerParent = transform;

        //SetDictionary();
    }

    /*private void SetDictionary()
    {
        foreach (TurretTowerInfo turretTowerInfo in turretTowerInfos)
        {
            turretTowerDic.Add(turretTowerInfo.towerName, turretTowerInfo);
        }
    }*/

    public void BuildTower()
    {
        
    }

    public void UpgradeTower(TurretTower turretTower)
    {
        if (turretTower.level >= turretTower.towerInfo.maxlevel)
        {
            //�ִ뷹���̸� ���׷��̵� ����
            return;
        }

        //��ȭ ���
        if (!TestGameManager.Instance.UseReSource(turretTower.towerInfo.price[turretTower.level]))
        {
            //��ȭ�� �����ϸ� ���׷��̵� ����
            return;
        }

        //Ÿ�� ������
        turretTower.level++;
        turretTower.SetInfo();
    }

    public void UpgradeTower(SpawnTower spawnTower)
    {
        if (spawnTower.level >= spawnTower.towerInfo.maxlevel)
        {
            //�ִ뷹���̸� ���׷��̵� ����
            return;
        }

        //��ȭ ���
        if (!TestGameManager.Instance.UseReSource(spawnTower.towerInfo.price[spawnTower.level]))
        {
            //��ȭ�� �����ϸ� ���׷��̵� ����
            return;
        }

        //Ÿ�� ������
        spawnTower.level++;
        spawnTower.SetInfo();
    }
}
