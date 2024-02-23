using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Achievement
{
    public int uid;
    public AchievementType type;
    public AchievementData achievementData;
    public Action<Achievement> Success_Achievement;

    public Achievement(AchievementType type, AchievementData data, Action<Achievement> action) {
        this.type = type;
        achievementData = data;
        Success_Achievement += action;
    }

    private void CheckProgress() {
        if(achievementData.Progress >= 1) {
            achievementData.IsLocked = true;
            Success_Achievement?.Invoke(this);
        }
    }

    public void UpdateValue(int value) {
        Debug.Log("Action Invoke!!!!!" + " " + achievementData.Progress);
        achievementData.TargetCount = value;

        CheckProgress();
    }
}
