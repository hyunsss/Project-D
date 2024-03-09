using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpawnUnitListPanel : MonoBehaviour
{
    [HideInInspector] public SpawnTower currentSpawnTower;
    Button[] buttons;
    public Sprite defaultsprite;
    //1 아처, 2 메이지, 3 워리어, 4 워커
    public List<Sprite> unitsprite = new List<Sprite>();

    private void Awake()
    {
        buttons = transform.Find("BackgroundPanel").GetComponentsInChildren<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        buttons[0].onClick.AddListener(() => CancelSpawnUnit(0));
        buttons[1].onClick.AddListener(() => CancelSpawnUnit(1));
        buttons[2].onClick.AddListener(() => CancelSpawnUnit(2));
        buttons[3].onClick.AddListener(() => CancelSpawnUnit(3));
        buttons[4].onClick.AddListener(() => CancelSpawnUnit(4));
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpawnTower != null)
        {
           
            for(int i = 0; i < 5; i++) {
                if(currentSpawnTower.createList.Count <= i) {
                    buttons[i].GetComponent<SpawnUnitButton>().spawnUnit = null;
                } else {
                    buttons[i].GetComponent<SpawnUnitButton>().spawnUnit = currentSpawnTower.createList[i];
                }
            }
            
        }

    }

    public void SetButtomImage(Button button, GameObject gameObject)
    {
        Image image = button.GetComponent<Image>();
        switch (gameObject.name)
        {
            case "Archer":
                image.sprite = unitsprite[0];
                break;
            case "Mage":
                image.sprite = unitsprite[1];
                break;
            case "Warrior":
                image.sprite = unitsprite[2];
                break;
            case "Worker":
                image.sprite = unitsprite[3];
                break;
            default: break;
        }
    }

    void CancelSpawnUnit(int i)
    {
        currentSpawnTower.CancelSpawnUnit(i);
    }



}
