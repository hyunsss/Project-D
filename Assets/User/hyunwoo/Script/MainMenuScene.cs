using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScene : MonoBehaviour
{
    public GameObject AchievePanel;
    public Button AchievePanelClose;

    public GameObject SettingPanel;
    public Button SettingPanelClose;
    public GameObject ScreenSettingPanel;
    public Button ScreenSettingButton;
    public GameObject SoundSettingPanel;
    public Button SoundSettingButton;
    public GameObject InformationPanel;
    public Button InformationButton;


    public Button GameStartButton;
    public Button SettingButton;
    public Button AchieveButton;
    public Button ExitGameButton;

    public Stack<GameObject> ActiveObject = new Stack<GameObject>();
    private List<GameObject> SettingPanelList = new List<GameObject>();

    private void Awake() {
        //업적 패널
        AchievePanel = transform.Find("Achieve Panel").gameObject;
        AchievePanelClose = transform.Find("Achieve Panel/Group/PlayerStatusPanel/CloseButton").GetComponent<Button>();
        //업적 패널

        //설정 패널
        SettingPanel = transform.Find("Setting Panel").gameObject;
        SettingPanelClose = transform.Find("Setting Panel/CloseButton").GetComponent<Button>();
        ScreenSettingPanel = transform.Find("Setting Panel/Panels/Main Panel/Screen Setting Panel").gameObject;
        SoundSettingPanel = transform.Find("Setting Panel/Panels/Main Panel/Sound Setting Panel").gameObject;
        InformationPanel = transform.Find("Setting Panel/Panels/Main Panel/Information Panel").gameObject;
        ScreenSettingButton = transform.Find("Setting Panel/Panels/Setting Menu Buttons/Screen Setting Button").GetComponent<Button>();
        SoundSettingButton = transform.Find("Setting Panel/Panels/Setting Menu Buttons/Sound Setting Button").GetComponent<Button>();
        InformationButton = transform.Find("Setting Panel/Panels/Setting Menu Buttons/Information Button").GetComponent<Button>();
        //설정 패널

        //메인 버튼
        GameStartButton = transform.Find("MainMenu/MainButtons/Game Start").GetComponent<Button>();
        SettingButton = transform.Find("MainMenu/MainButtons/Settings").GetComponent<Button>();
        AchieveButton = transform.Find("MainMenu/MainButtons/Achieve").GetComponent<Button>();
        ExitGameButton = transform.Find("MainMenu/MainButtons/Exit Game").GetComponent<Button>();
        //메인 버튼
    }


    // Start is called before the first frame update
    void Start()
    {
        AchievePanelClose.onClick.AddListener(() => ActiveObject.Pop().SetActive(false));
        SettingPanelClose.onClick.AddListener(() => ActiveObject.Pop().SetActive(false));
        
        GameStartButton.onClick.AddListener(() => GameManager.Instance.SceneChange(1));
        SettingButton.onClick.AddListener(() => {SettingPanel.SetActive(true); ActiveObject.Push(SettingPanel); });
        AchieveButton.onClick.AddListener(() => {AchievePanel.SetActive(true); ActiveObject.Push(AchievePanel); });
        ExitGameButton.onClick.AddListener(() => GameManager.Instance.ExitGame());

        SettingPanelList.Add(ScreenSettingPanel);
        SettingPanelList.Add(SoundSettingPanel);
        SettingPanelList.Add(InformationPanel);

        ScreenSettingButton.onClick.AddListener(() => SettingInteraction(ScreenSettingPanel));
        SoundSettingButton.onClick.AddListener(() => SettingInteraction(SoundSettingPanel));
        InformationButton.onClick.AddListener(() => SettingInteraction(InformationPanel));

    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(ActiveObject.Count > 0) {
                ActiveObject.Pop().SetActive(false);
            }
        }
    }

    private void SettingInteraction(GameObject targetpanel) {
        foreach(var item in SettingPanelList) {
            item.SetActive(false);
        }

        targetpanel.SetActive(true);
    }



}
