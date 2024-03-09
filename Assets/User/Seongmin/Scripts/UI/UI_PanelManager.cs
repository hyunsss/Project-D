using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_PanelManager : MonoBehaviour
{
    public static UI_PanelManager Instance;

   
    [SerializeField] 
    private GameObject          ui_CurrentPanel;
    [Header("Panels")]
    public GameObject           ui_TowerBuildPanel;
    public GameObject           ui_LevelUPPanel;
    public GameObject           ui_SpawnTowerPanel;

    //------------------INFO Panel-------------
    public GameObject           ui_WorkerUnitPanel;
    public GameObject           ui_MonsterINFO;
    public GameObject           ui_PlayerTowerInfo;
    public GameObject           ui_BattleUnitINFO;


    [Header("Event")]
    public UI_Boss_Text         bossPanel;
    public UI_GameObject_INFO   gameObjectINFO;
    public UI_UnitList          unitListPanel;
    public GameObject           dontBuildMessage;
    public MouseController      mouseController;
    [Header("Texts")]
    public TextMeshProUGUI      monsterText;
    public TextMeshProUGUI      unitText;
    public TextMeshProUGUI      towerText;
    public TextMeshProUGUI      scvText;
    public TextMeshProUGUI      moneyText;
    public TextMeshProUGUI      playTimeText_Sec;
    public TextMeshProUGUI      playTimeText_Min;
    public TextMeshProUGUI      playTimeText_Hour;

    private float               startTime;
    private int                 time_Sec;
    private int                 time_Min;
    private int                 time_Hour;
   
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
        ui_TowerBuildPanel.gameObject.SetActive(false);
        ui_BattleUnitINFO.gameObject.SetActive(false);
        ui_LevelUPPanel.gameObject.SetActive(false);

        bossPanel.gameObject.SetActive(false);
        gameObjectINFO.gameObject.SetActive(false);


    }
    private void Start()
    {
        startTime = Time.time;
    }
    void Update()
    {
        float playTime = Time.time - startTime;
        time_Hour = (int)(playTime / 3600);
        time_Min = (int)((playTime % 3600) / 60);
        time_Sec = (int)(playTime % 60);

        playTimeText_Sec.text = time_Sec.ToString();
        playTimeText_Min.text = time_Min.ToString();
        playTimeText_Hour.text = time_Hour.ToString();

        towerText.text = GameDB.Instance.tower_Player.Count.ToString();
        unitText.text = GameDB.Instance.unit_Player.Count.ToString();
        monsterText.text = GameDB.Instance.currentMonsterCount.ToString();
        scvText.text = GameDB.Instance.scv_Player.Count.ToString();
        moneyText.text = GameDB.Instance.OwnResource.mineral.ToString();

    }

    public void TowerBuildPanel_OPEN()
    {
        ui_CurrentPanel = ui_TowerBuildPanel;
        OpenPanel();
    }
    public void SpawnTowerPanel_OPEN()
    {
        ui_CurrentPanel = ui_SpawnTowerPanel;
        OpenPanel();
    }
    public void LevelUPPanel_OPEN()
    {

        ui_CurrentPanel = ui_LevelUPPanel;
        OpenPanel();
    }
    public void MonsterINFOPanel_OPEN()
    {
        ui_CurrentPanel = ui_MonsterINFO;
        gameObjectINFO = ui_MonsterINFO.GetComponent<UI_GameObject_INFO>();
        OpenPanel();
    }
    public void BattleUnitPanel_OPEN()
    {
        ui_CurrentPanel = ui_BattleUnitINFO;
        gameObjectINFO = ui_BattleUnitINFO.GetComponent<UI_GameObject_INFO>();
        OpenPanel();
    }
    public void PlayerTowerPanel_OPEN()
    {
        ui_CurrentPanel = ui_PlayerTowerInfo;
        gameObjectINFO = ui_PlayerTowerInfo.GetComponent<UI_GameObject_INFO>();
        OpenPanel();
    }
    public void WorkerUnitPanel_OPEN()
    {
        ui_CurrentPanel = ui_WorkerUnitPanel;
        gameObjectINFO = ui_WorkerUnitPanel.GetComponent<UI_GameObject_INFO>();
        OpenPanel();
    }



    private void OpenPanel()
    {
        PanelReSet();

        ui_CurrentPanel.SetActive(true);

    }
    public void PanelReSet()
    {
        ui_CurrentPanel.SetActive(false);
        ui_TowerBuildPanel.SetActive(false);
        ui_WorkerUnitPanel.SetActive(false);
        ui_SpawnTowerPanel.SetActive(false);

        ui_BattleUnitINFO.SetActive(false);
        ui_LevelUPPanel.SetActive(false);
        ui_MonsterINFO.SetActive(false);
        ui_PlayerTowerInfo.SetActive(false);
    }
    //----------------EVENT-----------------
    public void BossPanelSet()
    {
        bossPanel.gameObject.SetActive(true);
        
    }
    public void DontBuildMessage()
    {
        StartCoroutine(DontBuildMessageCoroutine());
    }
    public void ALLUnitSelect()
    {
        GameDB.Instance.unitlist.Clear();
        foreach (var unit in GameDB.Instance.unit_Player)
        {
            if (unit.gameObject.TryGetComponent(out Unit _unit))
            {
                
                GameDB.Instance.unitlist.Add(_unit);
            }
        }

        unitListPanel.UnitListDraw();
    }
    IEnumerator DontBuildMessageCoroutine()
    {
        dontBuildMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        dontBuildMessage.gameObject.SetActive(false);
    }
}
