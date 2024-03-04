using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.UI;


public class MonsterHPBar : MonoBehaviour
{
    public Image hpBar;

    Transform cam;
    private void Awake()
    {

        //gameObject.SetActive(false);
        cam = Camera.main.transform;
    }
    void Update()
    {
        transform.LookAt(transform.position + cam.rotation * Vector3.forward,cam.rotation*Vector3.up);
       
    }
    public void HPUpdate(float _currentHp, float _maxHp)
    {
        hpBar.fillAmount = _currentHp/_maxHp;
    }

}
