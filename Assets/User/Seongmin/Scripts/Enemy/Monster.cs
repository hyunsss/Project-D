using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Monster : MonoBehaviour
{
    [SerializeField]
    protected MonsterData       monsterData;
    [HideInInspector]
    public MonsterData          MonsterData { set { monsterData = value; } }
    [HideInInspector]
    public float                currentHp;
    public UnitAStar            aStar;
    public Animator             animator;
    public Transform            target;

    private Vector3             moveCheck;
    private float               repairing = 5f;
    private MonsterTower        tower = null;
    public enum State
    {
        chase,
        die,
        towerReqair,
        attack
    }
    public State state;

    protected void Awake()
    {
        aStar =  GetComponent<UnitAStar>();
        animator = GetComponent<Animator>();
        tower = GetComponent<MonsterTower>();
    }

    protected void Start()
    {
        moveCheck = transform.position;
        aStar.speed = monsterData.MonsterSpeed;
        currentHp = monsterData.MonsterHp;
        StartCoroutine(ChangeState());
    }
    protected void Update() {
        if(state == State.towerReqair )
        {

            transform.LookAt(tower.transform);
        }
        else
        {
            transform.LookAt(target);
        }

        float checkMove = Vector3.Distance(transform.position, moveCheck);
        if(checkMove > 0.08f)
        {
            animator.SetTrigger("isRun");
        }
        moveCheck = transform.position;
        
        transform.Rotate(new Vector3(0, 0, transform.rotation.z));
    }
    protected IEnumerator ChangeState()
    {
        while (state != State.die)
        {
            if (target != null&& state != State.towerReqair)
            {
                float checkAttack = Vector3.Distance(gameObject.transform.position, target.position);
                state = checkAttack <= 18f ? state = State.attack : state = State.chase;
            }
            // user Chase
            if (state == State.chase)
            {
                    TargetChase();
            }
            // Mpnster Attack
            else if (state == State.attack)
            {
                animator.SetTrigger("isAttack");
                Debug.Log("Àß ¸ÂÀ½");
            }
            // Monster Tower Repairing
            else if (state == State.towerReqair && tower != null) 
            {
                if(tower.TowerCurrnetHp < tower.TowerMaxHp)
                {
                    tower.RepairingTower(repairing);
                    animator.SetTrigger("isRepair");
                }
                // State Change
                else
                {
                    state = State.chase;  
                }
            }
           
            yield return new WaitForSeconds(1.5f);
        }
        if (state == State.die)
        {

            yield break;
        }
        else
        {
            yield break;
        }

    }

    protected void TargetChase()
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
    }

    protected void SetTowerTarget() //UserTower
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
    protected void SetUnitTarget() //UserUnit
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

    public void SetTowerObject(MonsterTower _tower) //MonsterTower
    {
        tower = _tower;
        state = State.towerReqair;
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
        LeanPool.Despawn(gameObject);
    }
}
