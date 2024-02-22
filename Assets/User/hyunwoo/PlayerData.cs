using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    //Player와 관련된 모든 데이터들을 여기에 저장
    private float playtime;
    private int killCount;
    private int deathCount;
    private int buildCount;
    ///... 등등
    ///
    public float PlayTime { get => playtime ; set => playtime = value;}
    public int KillCount { get => killCount; set => killCount = value;}
    public int DeathCount { get => deathCount; set => deathCount = value;}
    public int BuildCount { get => buildCount; set => buildCount = value;}
}
