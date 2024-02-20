using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShadow : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();

    // Update is called once per frame
    void Update()
    {
        foreach (Unit unit in units)
        {
            Vector3 direction = (unit.transform.position - Camera.main.transform.position).normalized;
                Debug.DrawLine(Camera.main.transform.position, unit.transform.position, Color.red);
            if(Physics.Raycast(Camera.main.transform.position, direction, out RaycastHit rayInfo, Mathf.Infinity, 1 << 29)) {
                UnitLight(rayInfo.collider.transform);
            }
        }
    }

    private void UnitLight(Transform center_trans) {
        Collider[] shadow_colls = Physics.OverlapSphere(center_trans.position, 30f, 1 << 29);

        for(int i = 0; i < shadow_colls.Length; i++) {
            MeshRenderer mesh = shadow_colls[i].GetComponent<MeshRenderer>();
            float alphaRadio = Vector3.Distance(center_trans.position, shadow_colls[i].transform.position) / 30f;
            Color originColor = mesh.material.color;
            float Tempalpha = Mathf.Min(originColor.a, alphaRadio - 0.4f);
            float alpha = Mathf.Max(0, Tempalpha);
            mesh.material.color = new Color(originColor.r, originColor.g, originColor.b, alpha);
        }

    }
}
