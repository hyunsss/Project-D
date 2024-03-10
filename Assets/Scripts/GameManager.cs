using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameSettings
{
    public bool isFirst = true;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameSettings settings = new GameSettings();

    float startTime;
    float currentTime;

    bool isFirst = true;
    bool isChangeScene;

    private void Awake()
    {
        isChangeScene = false;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return; // 여기를 추가합니다.
        }


    }

    private void Start()
    {
        if (isFirst == false)
        {
            PlayerData.Instance.LoadData();
            AchievementManager.Instance.LoadAchievements();
        }
        else
        {
            AchievementManager.Instance.ResetAchieve();
            PlayerData.Instance.ResetData();
            SaveGameSettings();
            PlayerData.Instance.SaveData();
            AchievementManager.Instance.SaveAchievements();
            isFirst = false;
        }
    }

    private void Update()
    {
        // if(isChangeScene == true) {
        //     if(SceneManager.GetActiveScene().buildIndex == 1) {
        //         GameSceneInit();
        //         isChangeScene = false;
        //     } else {

        //         isChangeScene = false;
        //     }
        // }


        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            currentTime = Time.time - startTime;
            if (currentTime >= 1f)
            {
                PlayerData.Instance.PlayTime += currentTime;
                startTime = Time.time;
            }
        }
    }

    public void SaveGameSettings()
    {
        string json = JsonUtility.ToJson(settings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", json);
    }

    public void LoadGameSettings()
    {
        string filePath = Application.persistentDataPath + "/gamesettings.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            settings = JsonUtility.FromJson<GameSettings>(json);
        }
    }

    public void SceneChange(int index)
    {
        SceneManager.sceneLoaded += GameSceneInit;
        SceneManager.LoadScene(index);
    }

    public void ExitGame()
    {
        SaveGameSettings();
        Application.Quit();
        PlayerData.Instance.SaveData();
        AchievementManager.Instance.SaveAchievements();
    }

    public void GameSceneInit(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1) // 예를 들어, 씬 인덱스 1에서만 캔버스를 찾고 싶을 경우
        {
            StartCoroutine(FindCanvasAfterDelay(0.5f));
            startTime = Time.time;
        }
        SceneManager.sceneLoaded -= GameSceneInit; // 이벤트 구독 해제를 잊지 마세요.
    }


    IEnumerator FindCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 지연
        AchievementManager.Instance.InitGameScene();
    }



}
