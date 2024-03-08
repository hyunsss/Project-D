using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class WallBridge : MonoBehaviour
{

    [HideInInspector] public Wall mainBodyWall;
    [HideInInspector] public Wall targetWall;
    [HideInInspector] public bool isMount;

    bool isEnable;
    private void OnEnable() {
        isEnable = false;
        Invoke("CheckWall", 1f);
    }

    private void OnDisable() {
        isEnable = false;
    }
    void Update()
    {
        if(isEnable == true && isMount == false) {
            if(mainBodyWall == null || targetWall == null) {
                LeanPool.Despawn(this);
            }  
        } 

        if(isEnable == true && isMount == true) {
            if(mainBodyWall == null) {
                LeanPool.Despawn(this);
            }
        }
            
    }

    void CheckWall() {
        if((mainBodyWall != null && targetWall != null) || isMount == true) isEnable = true;
    }
}
