using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private MonsterData monsterData;
    public MonsterData  MonsterData { set { monsterData = value; } }

    private float       currentHp;
    public UnitAStar    aStar;
    private Animator    animator;
    UnityEngine.Transform           target;

    public enum State
    {
        chase,
        die
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
        currentHp = monsterData.MonsterHp;
        aStar.speed = monsterData.MonsterSpeed;
         StartCoroutine(TargetChase());
    }
    void  Update() {
        transform.LookAt(target);
        transform.Rotate(new Vector3(0, transform.rotation.y, transform.rotation.z));
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
        float sortDistance = 99999;
        foreach (UnityEngine.Transform _target in GameManager.Instance.tower_Player)
        {
            print(_target.position);
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

        float sortDistance = Mathf.Infinity;
        foreach (UnityEngine.Transform _target in GameManager.Instance.unit_Player)
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
            state = State.die;
            animator.SetTrigger("isDie");
        }
    }
    private void Die()
    {
        gameObject.SetActive(false); // ������Ʈ Ǯ�� or ��Ǯ�� ���� ����
    }
}
