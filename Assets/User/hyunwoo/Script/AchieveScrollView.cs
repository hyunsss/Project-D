using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class AchieveScrollView : MonoBehaviour
{
    public GameObject AchievementPanel;

    private void OnEnable() {
        foreach (var item in AchievementManager.Instance.achievement_List)
        {
            GameObject panel = LeanPool.Spawn(AchievementPanel, transform, false);
            panel.GetComponent<AchievementPanel>().data = item.achievementData;
        }
        
    }
}
