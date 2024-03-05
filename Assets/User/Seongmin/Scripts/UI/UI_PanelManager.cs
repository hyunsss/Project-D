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
    private GameObject          ui_currentPanel = null;
    [Header("Panels")]
    public GameObject           ui_towerPanel;
    public GameObject           ui_unitPanel;
    public GameObject           ui_levelUPPanel;
    public UI_Boss_Text         ui_bossPanel;
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
        
        ui_currentPanel.gameObject.SetActive(false);
        ui_towerPanel.gameObject.SetActive(false);
        ui_unitPanel.gameObject.SetActive(false);
        ui_levelUPPanel.gameObject.SetActive(false);

    }
    void Update()
    {
        towerText.text = GameDB.Instance.tower_Player.Count.ToString();
        unitText.text = GameDB.Instance.unit_Player.Count.ToString();
        monsterText.text = GameDB.Instance.monsterCount.ToString();
        scvText.text = GameDB.Instance.scv_Player.Count.ToString();

       // if(buildTowerPanel.position.x > 730f )
        //{
       //    Vector3 newPos = buildTowerPanel.position + Vector3.left * speed *Time.deltaTime;
       //    buildTowerPanel.position = newPos;
     //  }
    }
    public void TowerPanel()
    {
        if(ui_currentPanel == ui_towerPanel)
        {
            PanelReSet();
        }
        else 
        {
            ui_currentPanel = ui_towerPanel;
            OpenPanel();
        }

    }
    public void UnitPanel()
    {
        if(ui_currentPanel == ui_unitPanel)
        {
            PanelReSet();
        }
        else
        {
            ui_currentPanel = ui_unitPanel;
            OpenPanel();
        }
        
    }
    public void LevelUPPanel()
    {
        if(ui_currentPanel == ui_levelUPPanel)
        {
            PanelReSet();
        }
        else
        {
            ui_currentPanel = ui_levelUPPanel;
            OpenPanel();
        }
    }

    private void OpenPanel()
    {
        PanelReSet();

        ui_currentPanel.SetActive(true);

    }
    private void PanelReSet()
    {
        ui_currentPanel.SetActive(false);
        ui_towerPanel.SetActive(false);
        ui_unitPanel.SetActive(false);
        ui_levelUPPanel.SetActive(false);
        
    }
    public void BossPanelSet()
    {
        ui_bossPanel.gameObject.SetActive(true);
        
    }
}
