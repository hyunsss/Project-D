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
    public GameObject           ui_TowerPanel;
    public GameObject           ui_BattleUnitPanel;
    public GameObject           ui_WorkerUnitPanel;
    public GameObject           ui_LevelUPPanel;
    public GameObject           ui_MonsterINFO;
    [Header("ChildScripts")]
    public UI_Boss_Text         bossPanel;
    public UI_Monster_INFO      monsterInfo;
    [Header("Texts")]
    public TextMeshProUGUI      monsterText;
    public TextMeshProUGUI      unitText;
    public TextMeshProUGUI      towerText;
    public TextMeshProUGUI      scvText;
    public TextMeshProUGUI      playTimeText;

    private float               startTime;
   
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
        
        ui_TowerPanel.gameObject.SetActive(false);
        ui_BattleUnitPanel.gameObject.SetActive(false);
        ui_LevelUPPanel.gameObject.SetActive(false);

        bossPanel.gameObject.SetActive(false);
        monsterInfo.gameObject.SetActive(false);


    }
    private void Start()
    {
        startTime = Time.time;
    }
    void Update()
    {
        float playTime = Time.time - startTime;
        playTimeText.text = playTime.ToString("F0");
        towerText.text = GameDB.Instance.tower_Player.Count.ToString();
        unitText.text = GameDB.Instance.unit_Player.Count.ToString();
        monsterText.text = GameDB.Instance.monsterCount.ToString();
        scvText.text = GameDB.Instance.scv_Player.Count.ToString();

    }

    public void TowerPanel()
    {
            ui_CurrentPanel = ui_TowerPanel;
            OpenPanel();
    }
    public void BattleUnitPanel()
    {
            ui_CurrentPanel = ui_BattleUnitPanel;
            OpenPanel();
    }
    public void WorkerUnitPanel()
    {
            ui_CurrentPanel = ui_WorkerUnitPanel;
            OpenPanel();
    }
    public void LevelUPPanel()
    {

            ui_CurrentPanel = ui_LevelUPPanel;
            OpenPanel();
    }
    public void MonsterINFOPanel()
    {
            ui_CurrentPanel = ui_MonsterINFO;
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
        ui_TowerPanel.SetActive(false);
        ui_BattleUnitPanel.SetActive(false);
        ui_LevelUPPanel.SetActive(false);
        ui_WorkerUnitPanel.SetActive(false);
        ui_MonsterINFO.SetActive(false);
    }
    public void BossPanelSet()
    {
        bossPanel.gameObject.SetActive(true);
        
    }
}
