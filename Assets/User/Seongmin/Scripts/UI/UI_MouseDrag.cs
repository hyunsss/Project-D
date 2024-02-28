using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MouseDrag : MonoBehaviour
{
    private RectTransform   dragRectangle;
    private Rect            dragRect;
    private Vector2         start = Vector2.zero;
    private Vector2         end = Vector2.zero;
    private Camera          mainCamera;




    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            start = Input.mousePosition;
            dragRect = new Rect();
        }

        if(Input.GetMouseButtonUp(0))
        {
            end = Input.mousePosition;
            //DrawDragRectangle();
        }

        if(Input.GetMouseButton(0)) 
        {
            start = end = Vector2.zero;
           // DrawDragRectangle();
        }
    }
}
