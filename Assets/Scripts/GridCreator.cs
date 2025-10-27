using UnityEditor.Rendering;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    [Header("Grid options")]
    [SerializeField]
    private float _cellSize = 10f;
    [SerializeField]
    private Color _color = Color.white;
    private readonly float _lineHeight = 2f;

    [Header("Terrain")]
    [SerializeField]
    private Terrain _terrain;

    private GameObject _gridObject;

    void Start()
    {
        ConfigureGridObject();
        CreateGrid();
    }


    private void ConfigureGridObject()
    {
        _gridObject = new GameObject("Grid");
        _gridObject.transform.SetParent(_terrain.transform);
        _gridObject.transform.localPosition = Vector3.zero;
    }

    private void CreateGrid()
    {

        float terrainWidth = _terrain.terrainData.size.x;
        float terrainLength = _terrain.terrainData.size.z;

        int cellsX = Mathf.RoundToInt(terrainWidth / _cellSize);
        int cellsZ = Mathf.RoundToInt(terrainLength / _cellSize);

        for (int i = 0; i <= cellsX; i++)
        {
            float x = i * _cellSize;

            RenderLine(
                new Vector3(x, _lineHeight, 0),
                new Vector3(x, _lineHeight, terrainLength)
            );
        }

        for (int i = 0; i <= cellsZ; i++)
        {
            float z = i * _cellSize;

            RenderLine(
                new Vector3(0, _lineHeight, z),
                new Vector3(terrainWidth, _lineHeight, z)
            );
        }
    }

    private void RenderLine(Vector3 start, Vector3 end)
    {
        GameObject lineObject = CreateLine();
        LineRenderer lineRenderer = CreateLineRenderer(lineObject);

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    private GameObject CreateLine()
    {
        GameObject lineObject = new("GridLine");
        lineObject.transform.SetParent(_gridObject.transform);

        return lineObject;
    }

    private LineRenderer CreateLineRenderer(GameObject lineObject)
    {
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = _color;
        lineRenderer.endColor = _color;
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 2;

        return lineRenderer;
    }
}