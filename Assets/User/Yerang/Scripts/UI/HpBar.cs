using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    private float maxHp;
    private float currenHp;

    public Slider hpBar;
    public Image fillImage;

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
