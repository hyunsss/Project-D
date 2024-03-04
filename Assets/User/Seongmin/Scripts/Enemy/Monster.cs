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
    public MonsterData          MonsterData { get { return monsterData; }  set { monsterData = value; } }
    [HideInInspector]
    public float                currentHp;
    public UnitAStar            aStar;
    public Animator             animator;
    public Transform            target;

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
        aStar =  GetComponent<UnitAStar>();
        animator = GetComponent<Animator>();
        tower = GetComponent<MonsterTower>();
        monsterHPBar = GetComponentInChildren<MonsterHPBar>();
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
        if(checkMove < 0.02f)
        {
            animator.SetTrigger("isIdle");
        }
        moveCheck = transform.position;
        transform.Rotate(new Vector3(0, 0, transform.rotation.z));
        
        if(moveSupport)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * monsterData.MonsterSpeed * Time.deltaTime;
        }
    }
    protected IEnumerator ChangeState()
    {
        while (state != State.die)
        {
            if (target != null&& state != State.towerReqair)
            {
                float checkAttack = Vector3.Distance(gameObject.transform.position, target.position);
                state = checkAttack <= 12f ? state = State.attack : state = State.chase;
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
                aStar.Chase(target);
            }
            else if(GameDB.Instance.unit_Player.Count > 0)
            {
                SetUnitTarget();
                aStar.Chase(target);
            }
        float checkMove = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if (25f > checkMove && checkMove > 10f)
        {
            transform.LookAt(target);
            moveSupport = true;
        }
        else
        {
            moveSupport = false;
        }

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
    }

    public void SetTowerObject(MonsterTower _tower) //MonsterTower
    {
        tower = _tower;
        state = State.towerReqair;
    }
    protected void HitDamage(float _damage)
    {
       currentHp -= _damage;
        monsterHPBar.HPUpdate(currentHp, monsterData.MonsterHp);
        if (currentHp <= 0)
        {
            Die();
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
