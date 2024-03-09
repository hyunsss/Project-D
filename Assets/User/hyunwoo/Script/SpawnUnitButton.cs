using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class SpawnUnitButton : MonoBehaviour
{
    public GameObject spawnUnit;

    private SpawnUnitListPanel spawnUnitListPanel;

    private void Awake() {
        spawnUnitListPanel = transform.parent.GetComponentInParent<SpawnUnitListPanel>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnUnit == null) GetComponent<Image>().sprite = spawnUnitListPanel.defaultsprite;
        else spawnUnitListPanel.SetButtomImage(GetComponent<Button>(), spawnUnit);
    }
}
