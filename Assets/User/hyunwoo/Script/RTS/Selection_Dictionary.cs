using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class Selection_Dictionary : MonoBehaviour
{
    public Dictionary<int, Unit> selectedTable = new Dictionary<int, Unit>();

    public void addSelected(GameObject go) {
        int id = go.GetInstanceID();

        if(!(selectedTable.ContainsKey(id)) && go.TryGetComponent(out Unit unit)) {
            selectedTable.Add(id, unit);
            go.gameObject.AddComponent<Selection_Component>();
            Debug.Log("Added " + id + " to selected dict");
        }
    }

    public void Deselect(int id) {
        selectedTable.Remove(id);
    }


    public void DeselectAll() {
        foreach(KeyValuePair<int, Unit> pair in selectedTable) {
            if(pair.Value != null) {
                Destroy(selectedTable[pair.Key].GetComponent<Selection_Component>());
            }
        }
        selectedTable.Clear();
    }
}
