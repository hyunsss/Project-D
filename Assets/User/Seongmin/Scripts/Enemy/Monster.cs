using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.AI.Navigation.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private MonsterData         monsterData;
    [HideInInspector]
    public MonsterData          MonsterData { get { return monsterData; }  set { monsterData = value; } }
    [HideInInspector]
    public float                currentHp;
    public Animator             animator;
    public Transform            target;

    private NavMeshAgent        nav;
    private Vector3             moveCheck;
    private float               repairing = 5f;
    private MonsterTower        tower = null;
    private MonsterHPBar        monsterHPBar;
    private bool                moveSupport = false;

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
        nav = GetComponent<NavMeshAgent>();
       
        animator = GetComponent<Animator>();
        tower = GetComponent<MonsterTower>();
        monsterHPBar = GetComponentInChildren<MonsterHPBar>();
    }

    protected void Start()
    {
        moveCheck = transform.position;

        nav.speed = MonsterData.MonsterSpeed;
        currentHp = MonsterData.MonsterHp;


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
        //----------------Move_Distance_Check----------------
        float checkMove = Vector3.Distance(transform.position, moveCheck);
        if(checkMove < 0.01f)
        {
            animator.SetTrigger("isIdle");
        }
        moveCheck = transform.position;
        transform.Rotate(new Vector3(0, 0, transform.rotation.z));
        //----------------Move_AI_Support----------------
     /*
            if (moveSupport)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                transform.position += direction *monsterData.MonsterSpeed * Time.deltaTime;
            }
       */ 
    }
    private void OnTriggerEnter(Collider _collider)
    {
        if(_collider.TryGetComponent<Installation>(out Installation tower))
        {
            state = State.attack;
        }
        if(_collider.TryGetComponent<Unit>(out Unit unit))
        {
            state  = State.attack;
        }
    }
    protected IEnumerator ChangeState()
    {
        while (state != State.die)
        {
            if(target != null) 
            {
                float checkAttack = Vector3.Distance(transform.position, target.position);
                if( checkAttack < 12f)
                {
                    state = State.attack;
                }
            }

            // user Chase
            if (state == State.chase)
            {
                    TargetChase();
            }
            // Monster Attack
            else if (state == State.attack)
            {
                if(target == null)
                {
                    state = State.chase;
                    nav.updatePosition = true;
                    break;
                }
                animator.SetTrigger("isAttack");
                Debug.Log("잘 맞음");
                nav.updateRotation = false;
                Attack();
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
            else
            {
                state = State.chase;
            }
            yield return new WaitForSeconds(0.7f);
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
            if(GameDB.Instance.tower_Player.Count > 0)
            {
                SetTowerTarget();
                
            }
            else if(GameDB.Instance.unit_Player.Count > 0)
            {
                SetUnitTarget();
                
            }
         ;
      
    }

    protected void SetTowerTarget() //UserTower
    {
        float sortDistance = 99999f;
        foreach (Transform _target in GameDB.Instance.tower_Player)
        {
            float targetDistance = Vector3.Distance(transform.position, _target.position);
                if(targetDistance < sortDistance)
                {
                    sortDistance = targetDistance;
               
                    target =  _target;
            }
        }
        nav.SetDestination(target.position+ (Vector3.right * 8) + (Vector3.forward * 8) + (Vector3.up * 4));
    }
    protected void SetUnitTarget() //UserUnit
    {

        float sortDistance = 99999f;
        foreach (Transform _target in GameDB.Instance.unit_Player)
        {
            float targetDistance = Vector3.Distance(transform.position, _target.position);
            if (targetDistance < sortDistance)
            {
                sortDistance = targetDistance;
                target = _target;
               
            }
        }
        nav.SetDestination(target.position);
    }

    public void SetTowerObject(MonsterTower _tower) //MonsterTower
    {
        tower = _tower;
        state = State.towerReqair;
    }
    public void HitDamage(float _damage)
    {
       currentHp -= _damage;
        monsterHPBar.HPUpdate(currentHp, monsterData.MonsterHp);
        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void Attack()
    {
        if(target != null)
        {
            if (target.TryGetComponent<Installation>(out Installation tower))
            {
                tower.GetDamage(monsterData.MonsterDamage);
                print("공격");
            }
            if(target.TryGetComponent<Unit>(out Unit unit))
            {
                unit.GetDamage(monsterData.MonsterDamage);
            }
        }

    }
    protected void Die()
    {
        state = State.die;
        animator.SetTrigger("isDie");
        GameDB.Instance.monsterCount--;
        LeanPool.Despawn(gameObject);
    }
}
