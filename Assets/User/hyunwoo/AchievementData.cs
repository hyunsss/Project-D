using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement Data", menuName = "Scriptable Object/Achievement Data", order = int.MaxValue)]
public class AchievementData : ScriptableObject
{
    [SerializeField] private int uid;
    [SerializeField] private AchievementType achievementType;
    [SerializeField] private string Achievement_name;
    [SerializeField] private string desc;
    [SerializeField] private bool isLocked;
    [SerializeField] private float progress;
    [SerializeField] private int count;
    [SerializeField] private int targetCount;

    public int Uid { get => uid; set => uid = value; }
    public AchievementType Type { get => achievementType; set => achievementType = value;}
    public string Name { get => Achievement_name; set => Achievement_name = value; }
    public string Desc { get => desc; set => desc = value; }
    public bool IsLocked { get => isLocked; set => isLocked = value; }
    public float Progress { get => progress; set { progress = value / targetCount; } }
    public int Count { get => count; set => count = value; }
    public int TargetCount { get => targetCount; set => targetCount = value; }
}
