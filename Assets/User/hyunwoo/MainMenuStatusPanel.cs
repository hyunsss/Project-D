using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class MainMenuStatusPanel : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = transform.Find("Context").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        SetText();
    }

    public void SetText() {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"플레이 타임 : {(int)(PlayerData.Instance.PlayTime / 60)}초");
        sb.AppendLine($"몬스터 킬 수 : {PlayerData.Instance.KillCount}");
        sb.AppendLine($"유닛 생산 : {PlayerData.Instance.UnitbuildCount}");
        sb.AppendLine($"건물 빌드 : {PlayerData.Instance.BuildCount}");
        sb.AppendLine($"죽은 횟수 : {PlayerData.Instance.DeathCount}");

        text.text = sb.ToString();
    }
}
