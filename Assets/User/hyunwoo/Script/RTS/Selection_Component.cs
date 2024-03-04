using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class Selection_Component : MonoBehaviour
{
    Outline outline;
    // Start is called before the first frame update
    void Start()
    {
        if(TryGetComponent(out outline)) {
            outline.OutlineMode = Outline.Mode.OutlineAll;
        }
    }

    private void OnDestroy() {
            
        if(outline != null) outline.OutlineMode = Outline.Mode.OutlineHidden;
    }
}
