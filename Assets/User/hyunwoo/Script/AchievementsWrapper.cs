using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AchievementsWrapper
{
    public List<SerializableAchievement> achievements;
}

[Serializable]
public class SerializableAchievement
{
    public int uid;
    public AchievementType type;
}