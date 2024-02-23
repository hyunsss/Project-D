using System;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public enum AchievementType { KillCount, PlayTime, DeathCount, BuildCount, }


public class AchievementManager : MonoBehaviour
{
    public GameObject AchievePopupObject;
    public Canvas canvas;
    public static AchievementManager Instance;
    public List<AchievementData> achievementData_List = new List<AchievementData>();
    public List<Achievement> achievement_List = new List<Achievement>();

    public Action<int> KillCount;
    public Action<int> PlayTime;
    public Action<int> DeathCount;
    public Action<int> BuildCount;

    private void Awake() {
        Instance = this;

    }

    private void Start() {
        InitAchieve();
    }

    private void InitAchieve() {
        foreach (AchievementData item in achievementData_List)
        {
            Achievement achievement = new Achievement(item.Type, item, EnableSuccessPanel);
            achievement_List.Add(achievement);
            AddAction(achievement);
        }
    }

    private void EnableSuccessPanel(Achievement achievement) {
        //성공 했을 경우 성공화면을 잠깐 띄워주는 함수
        GameObject popupClone = LeanPool.Spawn(AchievePopupObject, canvas.transform);
        AchievementPopup popup = popupClone.GetComponent<AchievementPopup>();
        AchievementData data = achievement.achievementData;
        string text = $"{data.Name}";

        popup.SetData(text, true);

        //그 후 성공한 업적은 삭제
        RemoveAchievement(achievement);
    }

    public void RemoveAchievement(Achievement achievement) {
        //리스트에서 삭제
        achievement_List.Remove(achievement);

        //액션에 들어가있는 함수 제거
        switch(achievement.type) {
            case AchievementType.PlayTime :
                PlayTime -= achievement.UpdateValue;
                break;
            case AchievementType.KillCount :
                KillCount -= achievement.UpdateValue;
                break;
            case AchievementType.DeathCount :
                DeathCount -= achievement.UpdateValue;
                break;
            case AchievementType.BuildCount :
                BuildCount -= achievement.UpdateValue;
                break;
        }
    }

    public void AddAchievement(Achievement achievement) {
        achievement_List.Add(achievement);
        AddAction(achievement);
    }

    public void AddAction(Achievement achievement) {

        //액션 추가
        switch(achievement.type) {
            case AchievementType.PlayTime :
                PlayTime += achievement.UpdateValue;
                break;
            case AchievementType.KillCount :
                KillCount += achievement.UpdateValue;
                break;
            case AchievementType.DeathCount :
                DeathCount += achievement.UpdateValue;
                break;
            case AchievementType.BuildCount :
                BuildCount += achievement.UpdateValue;
                break;
        }
    }

}
