using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if(Instance == null) Instance = this;
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);

    }
    private void Start()
    {
        
    }

    public void SceneChange(int index) {
        SceneManager.LoadScene(index);
    }

    public void ExitGame() {
        Application.Quit();
    }

}
