using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Lean.Pool;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public GameObject towerBullet;
    public Transform shotPoint;

    
    private int attackDamage;
    private float attackSpeed;
    private float shotRange;
    private float shotDelay;

   
    public int Damage { get => attackDamage; set => attackDamage = value;}
    public float BulletSpeed { get => attackSpeed; set => attackSpeed = value;}
    public float ShotRange { get => shotRange; set => shotRange = value;}
    public float ShotDelay { get => shotDelay; set => shotDelay = value;}

    Coroutine towerCoroutine;
    private Transform target;

    bool isShooting = false;

    void Update()
    {
        if (target == null)
        {
            Collider[] monsters = Physics.OverlapSphere(transform.position, shotRange, 1); // <-- Todo : 몬스터 레이어 마스크로 수정
            Transform temp_target = null;
            float temp_Distance = 999f;
            foreach (var enemy in monsters)
            {
                float Distance = Vector3.Distance(transform.position, enemy.gameObject.transform.position);
                if (Distance < shotRange && Distance < temp_Distance)
                {
                    temp_Distance = Distance;
                    temp_target = enemy.gameObject.transform;
                }
            }
            target = temp_target;
        }
        else
        {
            if(isShooting == false) {
                towerCoroutine = StartCoroutine(TowerShotCoruoutine());
            }
        }
    }

    IEnumerator TowerShotCoruoutine() {
        isShooting = true;
        //CreateBullet(towerType);

        yield return new WaitForSeconds(shotDelay);
        isShooting = false;
    }

    // private void CreateBullet(TowerType type)
    // {
    //     GameObject Bullet = LeanPool.Spawn(GameManager.Data.TowerBulletPrefab[type], shotPoint.position, shotPoint.rotation);
    //     TowerBullet bulletComponent = Bullet.GetComponent<TowerBullet>();
    //     bulletComponent.Damage = attackDamage;
    //     bulletComponent.target = target.transform;
    //     bulletComponent.Speed = attackSpeed;
    // }
}
