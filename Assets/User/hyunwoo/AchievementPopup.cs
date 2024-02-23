using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPopup : MonoBehaviour
{
    private Image checkImage;
    private TextMeshProUGUI descText;
    private Animator anim;

    private void Awake() {
        checkImage = transform.Find("CheckBox/Check").GetComponent<Image>();
        descText = transform.Find("DescText").GetComponent<TextMeshProUGUI>();
        anim = GetComponent<Animator>();
    }

    public void SetData(string desc, bool success) {
        if(success == true) {
            checkImage.gameObject.SetActive(true);
            StartCoroutine(PopupCoroutine());
        }
        descText.text = desc;
    }

    IEnumerator PopupCoroutine() {
        anim.SetTrigger("SlidePopup");

        yield return new WaitForSeconds(5f);

        LeanPool.Despawn(this);
    }
    
}
