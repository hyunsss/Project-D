using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDrawer : MonoBehaviour
{
    [SerializeField]
    protected LineRenderer arrowRenderer;
    [SerializeField]
    protected GameObject goalPointPrefab;

    protected RaycastHit hit;
    protected int targetLayerMask;

    protected Vector3 startPos;
    protected Vector3 endPos;

    private UnitMove unitMove;

    protected void Awake()
    {
        unitMove = GetComponent<UnitMove>();

        targetLayerMask = 1 << LayerMask.NameToLayer("Ground")
            | 1 << LayerMask.NameToLayer("Installation");
    }

    protected void OnMouseDown()
    {
        arrowRenderer.enabled = true;

        startPos = transform.position;
        startPos.y = 0.01f;
    }

    protected void OnMouseDrag()
    {
        startPos = transform.position;
        startPos.y = 0.01f;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 999, targetLayerMask))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                endPos = hit.point;
            }
            else
            {
                endPos = hit.collider.bounds.center;
                //endPos = hit.collider.ClosestPoint(startPos);
                //endPos = hit.transform.position;
            }
            endPos.y = 0.1f;
        }

        DrawArrow();
    }

    protected void OnMouseUp()
    {
        arrowRenderer.enabled = false;

        if (hit.transform == null)
        {
            return;
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
        foreach(Unit unit in GameDB.Instance.unitlist) {
            unit.GetComponent<UnitMove>().SetPriorityTarget(target);
        }
    }

    protected void DrawArrow()
    {
        float arrowheadSize = 0.5f;
        float arrowheadRate = (float)(arrowheadSize / Vector3.Distance(startPos, endPos));

        // ��           ��
        // ============��
        arrowRenderer.SetPosition(0, startPos);
        arrowRenderer.SetPosition(1, Vector3.Lerp(startPos, endPos, 0.999f - arrowheadRate));

        //             ���
        // ============��
        arrowRenderer.SetPosition(2, Vector3.Lerp(startPos, endPos, 1f - arrowheadRate));
        arrowRenderer.SetPosition(3, endPos);


        float arrowWidth = 0.2f;
        float arrowheadWidth = 0.5f;

        arrowRenderer.widthCurve = new AnimationCurve(
            // ��           ��
            // ============��
            new Keyframe(0, arrowWidth),
            new Keyframe(0.999f - arrowheadRate, arrowWidth),
            //             ���
            // ============��
            new Keyframe(1 - arrowheadRate, arrowheadWidth),
            new Keyframe(1, 0f)); //�β��� ���� �ٿ��� �ﰢ���������
    }
}
