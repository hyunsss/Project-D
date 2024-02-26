using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{

    private Collider[]      colliders;
    private float           range = 3f;
    private int             targetLayer = 1 << 7 | 1 << 30;
    private Monster         monster;
    public Transform        checkCollider;
    private void Awake()
    {
        monster = GetComponent<Monster>();
    }
    private void Update()
    {

        colliders = Physics.OverlapSphere(checkCollider.position+ Vector3.up , range, targetLayer);
        foreach(Collider collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Tower"))
            {
                monster.animator.SetTrigger("isAttack");
                Debug.Log("Àß ¸ÂÀ½");
            }
        }
    }
    
}
