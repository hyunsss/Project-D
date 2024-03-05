using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPanel : MonoBehaviour
{
    Image ClearImage;
    Image Label;
    TextMeshProUGUI AchieveTitle;
    TextMeshProUGUI AchieveContext;
    Image ProgressBar;
    TextMeshProUGUI ProgressTop;
    TextMeshProUGUI ProgressText;

    public AchievementData data;

    private void Awake() {
        ClearImage = transform.Find("Header/Clear Image").GetComponent<Image>();
        Label = transform.Find("Header/Label").GetComponent<Image>();
        AchieveTitle = transform.Find("Middle/Achieve Title").GetComponent<TextMeshProUGUI>();
        AchieveContext = transform.Find("Middle/Achieve Context").GetComponent<TextMeshProUGUI>();
        ProgressBar = transform.Find("Bottom/Progress Bar (Front)").GetComponent<Image>();
        ProgressTop = transform.Find("Bottom/Progress Top").GetComponent<TextMeshProUGUI>();
        ProgressText = transform.Find("Bottom/Progress Text").GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate() {
        SetData();        
    }

    private void SetData() {
        if(data.Progress >= 1) ClearImage.gameObject.SetActive(true);
        Label.color = SetLabelColor(data.Rate);
        AchieveTitle.text = data.Name;
        AchieveContext.text = data.Desc;
        ProgressBar.fillAmount = data.Progress;
        ProgressTop.text = $"{data.TargetCount} / {data.Count}";
        ProgressText.text = $"{data.Progress * 100}%";

        Debug.Log($"{data.Name}, {data.Progress}");
    }

    private Color SetLabelColor(AchieveRate rate) {
        Color color = Color.white;

        switch (rate) {
            case AchieveRate.UnRank : 
            color = Color.gray;
            break;
            case AchieveRate.Bronze : 
            color = new Color(205f/255f, 127f/255f, 50f/255f, 1f); 
            break;
            case AchieveRate.Silver : 
            color = new Color(192f/255f, 192f/255f, 192f/255f, 1f); 
            break;
            case AchieveRate.Gold : 
            color = new Color(255f/255f, 215f/255f, 0f, 1f); 
            break;
            case AchieveRate.Platinum : 
            color = new Color(62f/255f, 212f/255f, 190f/255f, 1f); 
            break;
            case AchieveRate.Diamond : 
            color = new Color(182f/255f, 242f/255f, 255f/255f, 1f); 
            break;
            case AchieveRate.Master : 
            color = new Color(139f/255f, 0f/255f, 255f/255f, 1f); 
            break;
            case AchieveRate.Challenger : 
            color = new Color(10f/255f, 200f/255f, 185f/255f, 1f); 
            break;
        }
        return color;
    }
}
