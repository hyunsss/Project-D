using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupUI : MonoBehaviour
{
    private Canvas canvas;

    private bool isCanvasFixed = false;

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
    }

    private void Update()
    {
        if(canvas != null && canvas.gameObject.activeSelf)
            canvas.transform.LookAt(Camera.main.transform, Camera.main.transform.up);
    }

    private void OnMouseEnter()
    {
        if (!isCanvasFixed)
            canvas.gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        if (!isCanvasFixed)
            canvas.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        isCanvasFixed = !isCanvasFixed;
        canvas.gameObject.SetActive(true);
    }
}
