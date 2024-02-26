using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Monster : MonoBehaviour
{
    [SerializeField]
    protected MonsterData     monsterData;
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
        towerReqair
    }
    public State state;

    protected void Awake()
    {
        aStar =  GetComponent<UnitAStar>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        state = State.chase;
        aStar.speed = monsterData.MonsterSpeed;
        currentHp = monsterData.MonsterHp;
        StartCoroutine(ChangeState());
    }
    protected void Update() {
        transform.LookAt(target);

        transform.Rotate(new Vector3(0, 0, transform.rotation.z));
    }
    protected IEnumerator ChangeState()
    {
        while (state != State.die)
        {
            if(state == State.chase)
            {
                StartCoroutine(TargetChase());
            }
            if (state == State.towerReqair)
            {
              //  StartCoroutine(TowerRepair(()); //TODO 매개변수 넣기
            }
        }
        if (state == State.die)
        {

            yield break;
        }

    }

    protected IEnumerator TargetChase()
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
    protected IEnumerator TowerRepair(GameObject _repairTower)
    {
        // 수리할 타워 정하고 
        yield return new WaitForSeconds(1f);
    }

    protected void SetTowerTarget()
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
    protected void SetUnitTarget()
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

    protected void HitDamage(float _damage)
    {
       currentHp -= _damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }
    protected void Die()
    {
        state = State.die;
        animator.SetTrigger("isDie");
        StopCoroutine(TargetChase());
        LeanPool.Despawn(gameObject);
    }
}
