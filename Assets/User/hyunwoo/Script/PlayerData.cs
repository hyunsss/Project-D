using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    //Player와 관련된 모든 데이터들을 여기에 저장
    private float playtime;
    private int killCount;
    private int deathCount;
    private int buildCount;
    private int unitbuildCount;
    ///... 등등
    ///
    public float PlayTime { get => playtime; set { playtime = value; AchievementManager.Instance.PlayTime?.Invoke((int)playtime); } }
    public int KillCount { get => killCount; set { killCount = value; AchievementManager.Instance.KillCount?.Invoke(killCount); } }
    public int DeathCount { get => deathCount; set { deathCount = value; AchievementManager.Instance.DeathCount?.Invoke(deathCount); } }
    public int BuildCount { get => buildCount; set { buildCount = value; AchievementManager.Instance.BuildCount?.Invoke(buildCount); } }
    public int UnitbuildCount { get => unitbuildCount; set { unitbuildCount = value; AchievementManager.Instance.BuildCount?.Invoke(unitbuildCount); } }
    
    public void SaveData()
    {
        string json = JsonUtility.ToJson(this, true);
        File.WriteAllText(Application.persistentDataPath + "/playerdata.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/playerdata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }

    public void ResetData() {
        playtime = 0;
        killCount = 0;
        deathCount = 0;
        buildCount = 0;
        unitbuildCount = 0;
    }

}
