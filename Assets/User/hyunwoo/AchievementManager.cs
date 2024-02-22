using System;
using System.Collections.Generic;
using UnityEngine;

public enum AchievementType { KillCount, PlayTime, Death, BuildCount, }


public class AchievementManager : MonoBehaviour
{
    public List<Achievement> achievement_List = new List<Achievement>();

    public Action<int> KillCount;
    public Action<int> PlayTime;
    public Action<int> DeathCount;
    public Action<int> BuildCount;

    private void Start() {
        foreach (Achievement item in achievement_List)
        {
            if(item.achievementData.IsLocked == true) {
                item.Success_Achievement += EnableSuccessPanel;
            }
        }
    }





    

    private void EnableSuccessPanel(Achievement achievement) {
        //성공 했을 경우 성공화면을 잠깐 띄워주는 함수

        //그 후 성공한 업적은 삭제
        RemoveAchievement(achievement);
    }

    public void RemoveAchievement(Achievement achievement) {
        achievement_List.Remove(achievement);
    }

    public void AddAchievement(Achievement achievement) {
        achievement_List.Add(achievement);
    }

}
