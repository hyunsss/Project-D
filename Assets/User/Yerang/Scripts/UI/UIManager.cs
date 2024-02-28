using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), 
                out RaycastHit hit, 999, 1 << LayerMask.NameToLayer("Tower")))
            {
                print($"Å¸¿ö Å¬¸¯µÊ: {hit.collider.gameObject.name}");
            }
        }
    }
}
