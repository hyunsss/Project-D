using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PanelManager : MonoBehaviour
{
    public RectTransform buildTowerPanel;
    public float speed = 50f;
    [SerializeField] 
    private GameObject currentPanel;
    private void Awake()
    {

    }
    void Update()
    {
       // if(buildTowerPanel.position.x > 730f )
        //{
       //    Vector3 newPos = buildTowerPanel.position + Vector3.left * speed *Time.deltaTime;
       //    buildTowerPanel.position = newPos;
     //  }
    }

    public void OpenPanel()
    {
        currentPanel.SetActive(true);
    }
    public void ClosePanel()
    {
        currentPanel.SetActive(false);
    }
}
