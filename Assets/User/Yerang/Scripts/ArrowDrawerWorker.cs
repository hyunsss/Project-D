using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ArrowDrawerWorker : ArrowDrawer
{
    private void Awake()
    {
        targetLayerMask = 1 << LayerMask.NameToLayer("Ground")
            | 1 << LayerMask.NameToLayer("Tower");
    }

    public new void OnMouseDrag()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 999, targetLayerMask))
        {
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Tower"))
            {
                endPos = hit.transform.position;
            }
            else
            {
                endPos = hit.point;
            }
            endPos.y = 0.1f;
        }

        DrawArrow();
    }

    public new void OnMouseUp()
    {
        arrowRenderer.enabled = false;

        ResetTarget();

        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Tower"))
        {
            target = hit.transform;
        }
        else
        {
            target =
            Instantiate(goalPointPrefab, endPos, Quaternion.identity).transform;
        }
    }

    public new void ResetTarget()
    {
        if (target != null)
        {
            if (target.gameObject.layer == LayerMask.NameToLayer("Tower"))
                target = null;
            else
                Destroy(target.gameObject);
        }
    }
}
