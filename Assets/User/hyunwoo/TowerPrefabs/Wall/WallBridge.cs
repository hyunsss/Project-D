using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class WallBridge : MonoBehaviour
{

    public Wall mainBodyWall;
    public Wall targetWall;

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
        if(isEnable == true) {
            if(mainBodyWall == null || targetWall == null) {
                LeanPool.Despawn(this);
            }  
        }
            
    }

    void CheckWall() {
        if(mainBodyWall != null && targetWall != null) isEnable = true;
    }
}
