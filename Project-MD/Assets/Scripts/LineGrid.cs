using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGrid : MonoBehaviour
{
    public int gridSize = 10;
    public float cellSize = 1.0f;
    public Material gridMaterial; // 적절한 메테리얼을 할당하세요.

    private Vector3 offset = new Vector3(5f, 0, 5f);
    void Start()
    {
        DrawGrid();
    }
    
    void DrawGrid()
    {
        for (int x = 0; x < gridSize + 1; x++)
        {
            DrawLine(new Vector3(x * cellSize, 0, 0) - offset, new Vector3(x * cellSize, 0, gridSize * cellSize) - offset);
        }

        for (int z = 0; z < gridSize + 1; z++)
        {
            DrawLine(new Vector3(0, 0, z * cellSize) - offset, new Vector3(gridSize * cellSize, 0, z * cellSize) - offset);
        }
    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        GameObject lineObject = new GameObject("GridLine");
        lineObject.transform.parent = transform;

        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.material = gridMaterial;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}