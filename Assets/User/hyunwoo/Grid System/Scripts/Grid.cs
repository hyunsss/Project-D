using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//그리드 클래스
public class GridXY : MonoBehaviour
{   //기본 Plane 오브젝트 스케일의 0.5배가 게임 내의 cell 1칸
    private int width;
    private int height;
    private float cellSize;
    private int[,] gridArray;

    public int Width => width;
    public int Height => height;
    public float CellSize => cellSize;
    public int[,] GridArray => gridArray;

    public void InitGrid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        //각 cell 위치를 저장하는 배열
        gridArray = new int[width, height];
    }

    public void GenerateGrid(GameObject cell)
    {
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                DrawGrid(GetWorldPosition(x, y), GetString(x, y));
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.red, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.red, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.red, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.red, 100f);
    }

    /// <summary>
    /// 포지션 x, y를 y축이 0인 Vector3 월드 좌표로 반환합니다.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, 0, y) * cellSize ;
    }

    public string GetString(int x, int y)
    {
        return $"{x}, {y}";
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        y = Mathf.FloorToInt(worldPosition.z / cellSize);
    }

    private void DrawGrid(Vector3 position, string Text)
    {
        GameObject newgameObject = new GameObject($"{Text}");
        newgameObject.transform.position = position + (new Vector3(cellSize, 0, cellSize) * 0.5f);
        newgameObject.transform.Rotate(new Vector3(90, 0, 0));
        TextMesh textMesh = newgameObject.AddComponent<TextMesh>();
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.text = Text;

        newgameObject.transform.parent = this.transform;
    }


}
