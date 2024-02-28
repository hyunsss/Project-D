using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BuildTower : MonoBehaviour
{
    public GameObject buildTowerPanel;
    public float speed = 50f;
    private void Awake()
    {
       // buildTowerPanel = GetComponent<TextMeshProUGUI>(); 
    }
    void Update()
    {
      //  if(buildTowerPanel.rectTransform.position.x > 730f )
       // {
      //      Vector3 newPos = buildTowerPanel.rectTransform.position + Vector3.left * speed *Time.deltaTime;
      //      buildTowerPanel.rectTransform.position = newPos;
        }
    }
}
