using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class InstallationManager : MonoBehaviour
{
    private static InstallationManager instance;
    public static InstallationManager Instance { get { return instance; } }

    private Transform installationParent;
    public Transform InstallationParent { get { return installationParent; } }

    //[SerializeField]
    //private TurretTowerInfo[] turretTowerInfos;
    //private SpawnTowerInfo[] spawnTowerInfos;
    //private FieldInfo[] fieldInfos;

    //private Dictionary<string, TurretTowerInfo> turretTowerDic = new Dictionary<string, TurretTowerInfo>();
    //private Dictionary<string, SpawnTowerInfo> spawnTowerDic = new Dictionary<string, SpawnTowerInfo>();
    //private Dictionary<string, FieldInfo> fieldDic = new Dictionary<string, FieldInfo>();

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

        installationParent = transform;

        //SetDictionary();
    }

    /*private void SetDictionary()
    {
        foreach (TurretTowerInfo turretTowerInfo in turretTowerInfos)
        {
            turretTowerDic.Add(turretTowerInfo.towerName, turretTowerInfo);
        }

        foreach (SpawnTowerInfo spawnTowerInfo in spawnTowerInfos)
        {
            spawnTowerDic.Add(spawnTowerInfo.towerName, spawnTowerInfo);
        }

        foreach (FieldInfo fieldInfo in fieldInfos)
        {
            fieldDic.Add(fieldInfo.fieldName, fieldInfo);
        }
    }*/

    public void BuildTower()
    {
        //SetTower
    }

    public void UpgradeTower(TurretTower turretTower)
    {
        if (turretTower.level >= turretTower.towerInfo.maxlevel)
        {
            //�ִ뷹���̸� ���׷��̵� ����
            return;
        }

        if (!GameDB.Instance.IsEnoughResource(turretTower.towerInfo.price[turretTower.level]))
        {
            //��ȭ�� �����ϸ� ���׷��̵� ����
            return;
        }

        //��ȭ ���
        GameDB.Instance.UseReSource(turretTower.towerInfo.price[turretTower.level]);

        //Ÿ�� ������
        turretTower.level++;
        turretTower.SetTower();
    }

    public void UpgradeTower(SpawnTower spawnTower)
    {
        if (spawnTower.level >= spawnTower.towerInfo.maxlevel)
        {
            //�ִ뷹���̸� ���׷��̵� ����
            return;
        }

        if (!GameDB.Instance.IsEnoughResource(spawnTower.towerInfo.prices[spawnTower.level]))
        {
            //��ȭ�� �����ϸ� ���׷��̵� ����
            return;
        }

        //��ȭ ���
        GameDB.Instance.UseReSource(spawnTower.towerInfo.prices[spawnTower.level]);

        //Ÿ�� ������
        spawnTower.level++;
        spawnTower.SetTower();
    }

    public void UpgradeField(Field field)
    {
        if (field.level >= field.fieldInfo.maxlevel)
        {
            //�ִ뷹���̸� ���׷��̵� ����
            return;
        }

        if (!GameDB.Instance.IsEnoughResource(field.fieldInfo.price[field.level]))
        {
            //��ȭ�� �����ϸ� ���׷��̵� ����
            return;
        }

        //��ȭ ���
        GameDB.Instance.UseReSource(field.fieldInfo.price[field.level]);

        //Ÿ�� ������
        field.level++;
        field.SetField();
    }
}
