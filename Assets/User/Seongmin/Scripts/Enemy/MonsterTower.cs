using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTower : MonoBehaviour
{
    [SerializeField]
    private float towerHp    =  100f;
    private MonsterSpawner      monsterSpawner;


    private void Awake()
    {
        monsterSpawner = GetComponent<MonsterSpawner>();       
    }

    private void Start()
    {
        StartCoroutine(spawnMonster());
    }
    IEnumerator spawnMonster()
    {
        while(towerHp > 0)
        {
            monsterSpawner.SpawnMonster();

            yield return new WaitForSeconds(1f);
        }
    }

    public void HitDamage(float _damage)
    {
        towerHp -= _damage;
        if(towerHp <= 0) 
        {

        }
    }
}
