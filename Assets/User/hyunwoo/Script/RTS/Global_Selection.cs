using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Global_Selection : MonoBehaviour
{
    Id_Dictionary id_table;
    Selection_Dictionary selected_table;
    RaycastHit hit;

    bool dragSelect;

    Vector3 p1;
    Vector3 p2;

    MeshCollider selectionBox;
    Mesh selectionMesh;

    Vector2[] corners;
    Vector3[] verts;
    Vector3[] vecs;
    Plane plane;

    public Transform SkyTransform;

    private void Start()
    {
        id_table = GetComponent<Id_Dictionary>();
        selected_table = GetComponent<Selection_Dictionary>();
        dragSelect = false;
        plane = MapManager.Instance.plane;
    }

    private Vector3 PlaneTransform()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float enter;
        Vector3 targetPos;
        //가상 평면에 레이와 마우스 클릭했을 때 쏜 레이가 맞았다면 그 아래 함수를 실행합니다.
        if (plane.Raycast(ray, out enter))
        {
            //레이가 만난 지점에서 해당 포지션을 가져옵니다. 
            targetPos = ray.GetPoint(enter);
        } else targetPos = Vector3.zero;
        return targetPos;
    }

    void Update()
    {
        //1. when left mouse button clicked (but not released)
        if (Input.GetMouseButtonDown(0))
        {
            p1 = Input.mousePosition;
        }

        //2. while left mouse button held
        if (Input.GetMouseButton(0))
        {
            if ((p1 - Input.mousePosition).magnitude > 40)
            {
                dragSelect = true;
            }
        }

        //3. when mouse button comes up
        if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            if (dragSelect == false) //single select
            {
                Ray ray = new Ray(SkyTransform.position, PlaneTransform() - SkyTransform.position);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 10))
                {
                    if (Input.GetKey(KeyCode.LeftShift)) //inclusive select
                    {
                        selected_table.addSelected(hit.transform.gameObject);
                    }
                    else //exclusive selected
                    {
                        selected_table.DeselectAll();
                        selected_table.addSelected(hit.transform.gameObject);
                    }
                }
                else //if we didnt hit something
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        //do nothing
                    }
                    else
                    {
                        selected_table.DeselectAll();
                        Debug.Log("mouse click (0)");
                    }
                }
            }
            else //marquee select
            {
                verts = new Vector3[4];
                vecs = new Vector3[4];
                int i = 0;
                p2 = Input.mousePosition;
                corners = getBoundingBox(p1, p2);

                foreach (Vector2 corner in corners)
                {
                    Ray ray = Camera.main.ScreenPointToRay(corner);

                    if (Physics.Raycast(ray, out hit, 50000.0f, (1 << 6)))
                    {
                        verts[i] = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                        vecs[i] = ray.origin - hit.point;
                        Debug.DrawLine(Camera.main.ScreenToWorldPoint(corner), hit.point, Color.blue, 1f);
                    }
                    i++;
                }

                //generate the mesh
                selectionMesh = generateSelectionMesh(verts, vecs);

                selectionBox = gameObject.AddComponent<MeshCollider>();
                selectionBox.sharedMesh = selectionMesh;
                if (selectionBox.sharedMesh != null)
                {
                    selectionBox.convex = true;
                    selectionBox.isTrigger = true;
                }

                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    selected_table.DeselectAll();
                }

                Destroy(selectionBox, 0.02f);

            }//end marquee select

            dragSelect = false;

        }

    }

    private void OnGUI()
    {
        if (dragSelect == true)
        {
            var rect = Utils.GetScreenRect(p1, Input.mousePosition);
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.8f));
        }
    }

    Vector2[] getBoundingBox(Vector2 p1, Vector2 p2)
    {
        // Min and Max to get 2 corners of rectangle regardless of drag direction.
        var bottomLeft = Vector3.Min(p1, p2);
        var topRight = Vector3.Max(p1, p2);

        // 0 = top left; 1 = top right; 2 = bottom left; 3 = bottom right;
        Vector2[] corners =
        {
            new Vector2(bottomLeft.x, topRight.y),
            new Vector2(topRight.x, topRight.y),
            new Vector2(bottomLeft.x, bottomLeft.y),
            new Vector2(topRight.x, bottomLeft.y)
        };
        return corners;

    }

    Mesh generateSelectionMesh(Vector3[] corners, Vector3[] vecs)
    {
        Vector3[] verts = new Vector3[8];
        int[] tris = { 0, 1, 2, 2, 1, 3, 4, 6, 0, 0, 6, 2, 6, 7, 2, 2, 7, 3, 7, 5, 3, 3, 5, 1, 5, 0, 1, 1, 4, 0, 4, 5, 6, 6, 5, 7 }; //map the tris of our cube

        for (int i = 0; i < 4; i++)
        {
            verts[i] = corners[i];
        }

        for (int j = 4; j < 8; j++)
        {
            verts[j] = corners[j - 4] + vecs[j - 4];
        }

        Mesh selectionMesh = new Mesh();
        selectionMesh.vertices = verts;
        selectionMesh.triangles = tris;

        return selectionMesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Unit unit))
        {
            selected_table.addSelected(unit.gameObject);

        }
    }

}
