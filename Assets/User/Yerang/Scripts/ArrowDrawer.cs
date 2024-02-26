using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

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

    public Transform target = null;

    private void Awake()
    {
        targetLayerMask = 1 << LayerMask.NameToLayer("Ground");
    }

    public void OnMouseDown()
    {
        arrowRenderer.enabled = true;

        startPos = transform.position;
        startPos.y = 0.01f;
    }

    public void OnMouseDrag()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 999, targetLayerMask))
        {
            endPos = hit.point;
            endPos.y = 0.1f;
        }

        DrawArrow();
    }

    public virtual void OnMouseUp()
    {
        arrowRenderer.enabled = false;

        if (target != null)
        {
            Destroy(target.gameObject);
        }

        target = 
            Instantiate(goalPointPrefab, endPos, Quaternion.identity).transform;
    }

    private void DrawArrow()
    {
        float arrowheadSize = 0.5f;
        float arrowheadRate = (float)(arrowheadSize / Vector3.Distance(startPos, endPos));

        // ↓           ↓
        // ============▷
        arrowRenderer.SetPosition(0, startPos);
        arrowRenderer.SetPosition(1, Vector3.Lerp(startPos, endPos, 0.999f - arrowheadRate));

        //             ↓↓
        // ============▷
        arrowRenderer.SetPosition(2, Vector3.Lerp(startPos, endPos, 1f - arrowheadRate));
        arrowRenderer.SetPosition(3, endPos);


        float arrowWidth = 0.2f;
        float arrowheadWidth = 0.5f;

        arrowRenderer.widthCurve = new AnimationCurve(
            // ↓           ↓
            // ============▷
            new Keyframe(0, arrowWidth),
            new Keyframe(0.999f - arrowheadRate, arrowWidth),
            //             ↓↓
            // ============▷
            new Keyframe(1 - arrowheadRate, arrowheadWidth),
            new Keyframe(1, 0f)); //두께를 점점 줄여서 삼각형모양으로
    }

}
