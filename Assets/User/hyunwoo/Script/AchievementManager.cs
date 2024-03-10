using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;

public enum AchievementType { KillCount, PlayTime, DeathCount, BuildCount, UnitBuildCount}

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

    private string filePath;

    private void Awake()
    {
        Instance = this;
        filePath = Application.persistentDataPath + "/achievements.json";
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitAchieve();
    }

    public void InitGameScene() {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    private void InitAchieve()
    {
        foreach (AchievementData item in achievementData_List)
        {
            Achievement achievement = new Achievement(item.Type, item, EnableSuccessPanel);
            achievement_List.Add(achievement);
            AddAction(achievement);
        }
    }

    private void EnableSuccessPanel(Achievement achievement)
    {
        //성공 했을 경우 성공화면을 잠깐 띄워주는 함수
        GameObject popupClone = LeanPool.Spawn(AchievePopupObject, canvas.transform);
        AchievementPopup popup = popupClone.GetComponent<AchievementPopup>();
        AchievementData data = achievement.achievementData;
        string text = $"{data.Name}";

        popup.SetData(text, true);

        //그 후 성공한 업적은 삭제
        RemoveAchievement(achievement);
    }

    public void RemoveAchievement(Achievement achievement)
    {
        //리스트에서 삭제
        achievement_List.Remove(achievement);

        //액션에 들어가있는 함수 제거
        switch (achievement.type)
        {
            case AchievementType.PlayTime:
                PlayTime -= achievement.UpdateValue;
                break;
            case AchievementType.KillCount:
                KillCount -= achievement.UpdateValue;
                break;
            case AchievementType.DeathCount:
                DeathCount -= achievement.UpdateValue;
                break;
            case AchievementType.BuildCount:
                BuildCount -= achievement.UpdateValue;
                break;
            case AchievementType.UnitBuildCount:
                BuildCount -= achievement.UpdateValue;
                break;
        }
    }

    public void AddAchievement(Achievement achievement)
    {
        achievement_List.Add(achievement);
        AddAction(achievement);
    }

    public void AddAction(Achievement achievement)
    {

        //액션 추가
        switch (achievement.type)
        {
            case AchievementType.PlayTime:
                PlayTime += achievement.UpdateValue;
                break;
            case AchievementType.KillCount:
                KillCount += achievement.UpdateValue;
                break;
            case AchievementType.DeathCount:
                DeathCount += achievement.UpdateValue;
                break;
            case AchievementType.BuildCount:
                BuildCount += achievement.UpdateValue;
                break;
            case AchievementType.UnitBuildCount:
                BuildCount += achievement.UpdateValue;
                break;
        }
    }

    public void SaveAchievements()
    {
        List<SerializableAchievement> serializableAchievements = new List<SerializableAchievement>();
        foreach (Achievement achievement in achievement_List)
        {
            serializableAchievements.Add(new SerializableAchievement
            {
                uid = achievement.achievementData.Uid,
                type = achievement.type,
                // 다른 필요한 데이터 필드 초기화
            });
        }
        AchievementsWrapper wrapper = new AchievementsWrapper { achievements = serializableAchievements };
        string jsonData = JsonUtility.ToJson(wrapper);
        File.WriteAllText(filePath, jsonData);
        Debug.Log("데이터 세이브 완료!");
    }

    public void LoadAchievements()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            AchievementsWrapper wrapper = JsonUtility.FromJson<AchievementsWrapper>(jsonData);
            achievement_List.Clear(); // 기존 목록을 클리어합니다.

            foreach (var serializableAchievement in wrapper.achievements)
            {
                // UID를 사용하여 해당 AchievementData 찾기
                AchievementData foundData = achievementData_List.Find(data => data.Uid == serializableAchievement.uid);
                if (foundData == null)
                {
                    // 찾지 못했다면, 새로운 AchievementData를 생성하고 초기화합니다.
                    // 이 부분은 게임의 구체적인 요구사항에 따라 다를 수 있습니다.
                    continue;
                }

                // 찾은 또는 생성한 AchievementData를 사용하여 Achievement 객체 재구성
                Achievement newAchievement = new Achievement(serializableAchievement.type, foundData, EnableSuccessPanel);
                Debug.Log("새로운 업적 데이터!" + newAchievement.type + "," + newAchievement.achievementData.Uid);
                AddAchievement(newAchievement);

                // 필요한 경우 여기에서 Achievement에 대한 추가적인 설정을 수행할 수 있습니다.
            }

            // Achievement 객체들이 재구성되었으니, 필요한 액션 연결 등의 초기화 로직을 수행합니다.
        }
    }

    public void ResetAchieve() {
        foreach (AchievementData item in achievementData_List) {
            item.TargetCount = 0;
        }
    }

    
}
