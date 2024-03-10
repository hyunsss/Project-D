using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    float startTime;
    float currentTime;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);

    }

    private void Update() {
        if(SceneManager.GetActiveScene().buildIndex == 1) {
            currentTime = Time.time - startTime;
            if(currentTime >= 1f) {
                PlayerData.Instance.PlayTime += currentTime;
                startTime = Time.time;
            }
        }
    }

    public void SceneChange(int index) {
        SceneManager.LoadScene(index);

        if(index == 1) startTime = Time.time;
    }

    public void ExitGame() {
        Application.Quit();
        PlayerData.Instance.SaveData();
        AchievementManager.Instance.SaveAchievements();
    }

}
