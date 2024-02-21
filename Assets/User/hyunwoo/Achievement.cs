using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Achievement
{
    public AchievementData achievementData;
    public Action<Achievement> Success_Achievement;

    private void CheckProgress() {
        if(achievementData.Progress >= 1) {
            achievementData.IsLocked = true;
            Success_Achievement?.Invoke(this);
        }
    }

    private void UpdateValue(int value) {
        achievementData.Count += value;
    }
}
