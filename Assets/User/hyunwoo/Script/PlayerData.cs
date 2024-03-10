using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    private void Awake() {
        Instance = this;
    }    


    //Player와 관련된 모든 데이터들을 여기에 저장
    private float playtime;
    private int killCount;
    private int deathCount;
    private int buildCount;
    private int unitbuildCount;
    ///... 등등
    ///
    public float PlayTime { get => playtime; set { playtime = value; AchievementManager.Instance.PlayTime?.Invoke((int)playtime);} }
    public int KillCount { get => killCount; set { killCount = value; AchievementManager.Instance.KillCount?.Invoke(killCount);} }
    public int DeathCount { get => deathCount; set { deathCount = value; AchievementManager.Instance.DeathCount?.Invoke(deathCount);}}
    public int BuildCount { get => buildCount; set { buildCount = value;AchievementManager.Instance.BuildCount?.Invoke(buildCount);}}
    public int UnitbuildCount { get => unitbuildCount; set { unitbuildCount = value;AchievementManager.Instance.BuildCount?.Invoke(unitbuildCount);}}

}
