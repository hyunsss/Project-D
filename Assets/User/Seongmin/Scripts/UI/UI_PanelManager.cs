using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_PanelManager : MonoBehaviour
{
    public static UI_PanelManager Instance;

    
    public RectTransform        buildTowerPanel;
    public float                speed = 50f;
    [SerializeField] 
    private GameObject          ui_CurrentPanel = null;
    [Header("Panels")]
    public GameObject           ui_TowerPanel;
    public GameObject           ui_BattleUnitPanel;
    public GameObject           ui_WorkerUnitPanel;
    public GameObject           ui_LevelUPPanel;
    public UI_Boss_Text         ui_BossPanel;
    [Header("Texts")]
    public TextMeshProUGUI      monsterText;
    public TextMeshProUGUI      unitText;
    public TextMeshProUGUI      towerText;
    public TextMeshProUGUI      scvText;

    private bool                isOpened = false;
   
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
        
        ui_CurrentPanel.gameObject.SetActive(false);
        ui_TowerPanel.gameObject.SetActive(false);
        ui_BattleUnitPanel.gameObject.SetActive(false);
        ui_LevelUPPanel.gameObject.SetActive(false);

    }
    void Update()
    {
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
        
    }
    public void BossPanelSet()
    {
        ui_BossPanel.gameObject.SetActive(true);
        
    }
}
