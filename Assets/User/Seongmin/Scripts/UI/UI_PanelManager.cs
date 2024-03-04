using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_PanelManager : MonoBehaviour
{
    public RectTransform buildTowerPanel;
    public float speed = 50f;
    [SerializeField] 
    private GameObject currentPanel;
    [Header("Panels")]
    public GameObject towerPanel;
    public GameObject unitPanel;
    public GameObject levelUPPanel;
    [Header("Texts")]
    public TextMeshProUGUI monsterText;
    public TextMeshProUGUI unitText;
    public TextMeshProUGUI towerText;
    
    private bool isOpen = false;
    private void Awake()
    {
        currentPanel.gameObject.SetActive(false);
        towerPanel.gameObject.SetActive(false);
        unitPanel.gameObject.SetActive(false);
        levelUPPanel.gameObject.SetActive(false);

    }
    void Update()
    {
        towerText.text = GameDB.Instance.tower_Player.Count.ToString();
        unitText.text = GameDB.Instance.unit_Player.Count.ToString();
        monsterText.text = GameDB.Instance.monsterCount.ToString();

       // if(buildTowerPanel.position.x > 730f )
        //{
       //    Vector3 newPos = buildTowerPanel.position + Vector3.left * speed *Time.deltaTime;
       //    buildTowerPanel.position = newPos;
     //  }
    }
    public void TowerPanel()
    {
        currentPanel = towerPanel;
        OpenPanel();
    }
    public void UnitPanel()
    {
        currentPanel = unitPanel;
        OpenPanel();
    }
    public void LevelUPPanel()
    {
        currentPanel = levelUPPanel;
        OpenPanel();
    }

    private void OpenPanel()
    {
        PanelReSet();
        currentPanel.SetActive(true);
          

    }
    private void PanelReSet()
    {
        currentPanel.SetActive(false);
        towerPanel.SetActive(false);
        unitPanel.SetActive(false);
        levelUPPanel.SetActive(false);
    }
}
