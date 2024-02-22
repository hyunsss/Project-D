using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private MonsterData monsterData;
    public MonsterData MonsterData { set { monsterData = value; } }


    public UnitAStar aStar;
    Transform target;

    public enum State
    {
        chase,
        die
    }
    public State state;

    private void Awake()
    {
        aStar =  GetComponent<UnitAStar>();
    }
    private void Start()
    {
        state = State.chase;
         StartCoroutine(TargetChase());
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
            yield return new WaitForSeconds(1f);
        }
    }
    private void SetTowerTarget()
    {
        float sortDistance = 99999;
        foreach (Transform _target in GameManager.Instance.tower_Player)
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
}
