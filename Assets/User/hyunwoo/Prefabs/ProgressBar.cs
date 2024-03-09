using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image fillImage;

    private void Awake() {
        fillImage = transform.Find("FillAmount Image").GetComponent<Image>();
    }

    public void FillAmount(float progress) {
        fillImage.fillAmount = 1 - progress;
    }

    public void FillReset() {
        fillImage.fillAmount = 1;
        gameObject.SetActive(true) ;
    }
}
