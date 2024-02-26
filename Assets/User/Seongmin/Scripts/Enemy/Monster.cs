using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private MonsterData     monsterData;
    [HideInInspector]
    public MonsterData      MonsterData { set { monsterData = value; } }
    [HideInInspector]
    public float            currentHp;
    public UnitAStar        aStar;
    public Animator         animator;
    public Transform        target;
    public enum State
    {
        chase,
        die,
        attack,
        tower
    }
    public State state;

    private void Awake()
    {
        aStar =  GetComponent<UnitAStar>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        state = State.chase;
        aStar.speed = monsterData.MonsterSpeed;
        currentHp = monsterData.MonsterHp;
        StartCoroutine(TargetChase());
    }
    private void Update() {
        transform.LookAt(target);

        transform.Rotate(new Vector3(0, 0, transform.rotation.z));
    }

    IEnumerator TargetChase()
    {
        while(state != State.die) 
        {
            if(GameManager.Instance.tower_Player.Count > 0)
            {
                SetTowerTarget();
                aStar.Chase(target);
            }
            else if(GameManager.Instance.unit_Player.Count > 0)
            {
                SetUnitTarget();
                aStar.Chase(target);
            }
            yield return new WaitForSeconds(0.7f);
        }
    }

    private void SetTowerTarget()
    {
        float sortDistance = 99999f;
        foreach (Transform _target in GameManager.Instance.tower_Player)
        {
            float targetDistance = Vector3.Distance(transform.position, _target.position);
                if(targetDistance < sortDistance)
                {
                    sortDistance = targetDistance;
                target =  _target;
                }
        }
    }
    private void SetUnitTarget()
    {

        float sortDistance = 99999f;
        foreach (Transform _target in GameManager.Instance.unit_Player)
        {
            float targetDistance = Vector3.Distance(transform.position, _target.position);
            if (targetDistance < sortDistance)
            {
                sortDistance = targetDistance;
                target = _target;
            }
        }
    }

    private void HitDamage(float _damage)
    {
       currentHp -= _damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        state = State.die;
        animator.SetTrigger("isDie");
        StopCoroutine(TargetChase());
        LeanPool.Despawn(gameObject);
    }
}
