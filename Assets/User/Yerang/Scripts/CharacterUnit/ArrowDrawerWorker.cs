using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ArrowDrawerWorker : ArrowDrawer
{
    private WorkerUnit workerUnit;
    private WorkerUnitMove workerUnitMove;

    private void Awake()
    {
        workerUnit = GetComponent<WorkerUnit>();
        workerUnitMove = GetComponent<WorkerUnitMove>();

        targetLayerMask = 1 << LayerMask.NameToLayer("Ground")
            | 1 << LayerMask.NameToLayer("Tower")
            | 1 << LayerMask.NameToLayer("TowerBeingBuilt")
            | 1 << LayerMask.NameToLayer("Field");
    }

    public new void OnMouseDrag()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 999, targetLayerMask))
        {
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                endPos = hit.point;
            }
            else
            {
                endPos = hit.transform.position;
            }
            endPos.y = 0.1f;
        }

        DrawArrow();
    }

    

    public new void OnMouseUp()
    {
        arrowRenderer.enabled = false;

        if(hit.transform == null)
        {
            return;
        }

        if(workerUnit.state != WorkerUnit.State.Idle)
        {
            workerUnit.Decollocate();
        }

        Transform target;
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            target =
                Lean.Pool.LeanPool.Spawn(goalPointPrefab, endPos, Quaternion.identity).transform;
        }
        else
        {
            target = hit.transform;
        }
        workerUnitMove.SetTarget(target);
    }
}
