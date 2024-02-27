using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField]
    private Collider[]      colliders;
    private float           range = 3f;
    private int             targetLayer = 1 << 7 | 1 << 30;
    private Monster         monster;
    public GameObject       checkCollider;
    private void Awake()
    {
        monster = GetComponent<Monster>();
    }
    private void Update()
    {

        
        colliders = Physics.OverlapSphere(checkCollider.transform.position+ Vector3.up , range, targetLayer);
        foreach(Collider collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Tower")
                || collider.gameObject.layer == LayerMask.NameToLayer("Build"))
            {
                monster.animator.SetTrigger("isAttack");
                Debug.Log("Àß ¸ÂÀ½");
            }
        }

    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(checkCollider.transform.position + Vector3.up, range * 2);
    }
    */


}
