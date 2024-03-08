using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class Selection_Dictionary : MonoBehaviour
{
    public UI_UnitList unitListData;
    public void addSelected(GameObject go) {
        int id = go.GetInstanceID();
        if(!(GameDB.Instance.selectedTable.ContainsKey(id)) && go.TryGetComponent(out Unit unit)) {
            GameDB.Instance.selectedTable.Add(id, unit);
            GameDB.Instance.unitlist.Add(unit);
            go.gameObject.AddComponent<Selection_Component>();
            Debug.Log("Added " + id + " to selected dict");

            unitListData.UnitListDraw();
        }
    }
    
    public void DeselectAll() {
        foreach(KeyValuePair<int, Unit> pair in GameDB.Instance.selectedTable) {
            if(pair.Value != null) {
                Destroy(GameDB.Instance.selectedTable[pair.Key].GetComponent<Selection_Component>());
            }
        }
        GameDB.Instance.selectedTable.Clear();
        GameDB.Instance.unitlist.Clear();
    }
}
