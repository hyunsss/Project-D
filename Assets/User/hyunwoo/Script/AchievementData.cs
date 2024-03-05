using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AchieveRate { UnRank, Bronze, Silver, Gold, Platinum, Diamond, Master, Challenger }

[CreateAssetMenu(fileName = "Achievement Data", menuName = "Scriptable Object/Achievement Data", order = int.MaxValue)]
public class AchievementData : ScriptableObject
{
    [Header("고유 아이디")]
    [SerializeField] private int uid;
    [Header("업적 등급")]
    [SerializeField] private AchieveRate rate;
    [Header("업적 타입 설정")]
    [SerializeField] private AchievementType achievementType;
    [Header("제목")]
    [SerializeField] private string Achievement_name;
    [Header("업적 설명문")]
    [SerializeField] private string desc;
    [Header("성공 여부")]
    [SerializeField] private bool isLocked;
    [Header("목표 달성량 수치")]
    [SerializeField] private int count;
    [Header("현재 달성량")]
    [SerializeField] private int targetCount;

    public int Uid { get => uid; set => uid = value; }
    public AchieveRate Rate { get => rate; }
    public AchievementType Type { get => achievementType; set => achievementType = value;}
    public string Name { get => Achievement_name; set => Achievement_name = value; }
    public string Desc { get => desc; set => desc = value; }
    public bool IsLocked { get => isLocked; set => isLocked = value; }
    public float Progress { get => (float)TargetCount / (float)Count;}
    public int Count { get => count; set => count = value; }
    public int TargetCount { get => targetCount; set => targetCount = value; }
}
