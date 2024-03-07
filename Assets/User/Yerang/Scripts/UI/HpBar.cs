using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    private float maxHp;
    private float currenHp;

    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private Image fillImage;

    private void Awake()
    {
        //hpBar = GetComponent<Slider>();
        fillImage = hpBar.transform.GetChild(1).GetChild(0).GetComponent<Image>();
    }

    public void SetHpBar(float currentHp, float maxHp)
    {
        this.currenHp = currentHp;
        this.maxHp = maxHp;
        hpBar.value = currenHp / maxHp;

        if(hpBar.value > 0.3)
        {
            fillImage.color = Color.white;
        }
        else
        {
            fillImage.color = Color.red;
        }
    }
}
