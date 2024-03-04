using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UIElements;

public class MonsterHPBar : MonoBehaviour
{
    public Slider hpBar;
    Monster monster;

    Transform cam;
    private void Awake()
    {
        monster = GetComponent<Monster>();
        //gameObject.SetActive(false);
        cam = Camera.main.transform;
    }
    void Update()
    {
        transform.LookAt(transform.position + cam.rotation * Vector3.forward,cam.rotation*Vector3.up);
        hpBar.value = monster.currentHp / monster.MonsterData.MonsterHp;
    }

}
