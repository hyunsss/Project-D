using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTower : MonoBehaviour
{
    [SerializeField]
    private float towerMaxHp = 100f;
    [SerializeField]
    private float towerCurrnetHp;
   
    private MonsterSpawner      monsterSpawner;


    private void Awake()
    {
        monsterSpawner = GetComponent<MonsterSpawner>();       
    }

    private void Start()
    {
        towerCurrnetHp = towerMaxHp;
        StartCoroutine(spawnMonster());
    }
    IEnumerator spawnMonster()
    {
        while(towerCurrnetHp > 0)
        {
            monsterSpawner.SpawnMonster();
            
            yield return new WaitForSeconds(1f);
        }
    }
    private void SpawnTowerKeeper()
    {
        monsterSpawner.SpawnTowerKeeper(gameObject); //TODO Ÿ�� Ű�ۿ��� Ÿ���� Ÿ���� �Ű����� ������ Ȱ���ϱ�
    }

    public void HitDamage(float _damage)
    {
        towerCurrnetHp -= _damage;
        if(towerCurrnetHp <= 0) 
        {
            
        }
    }
}
