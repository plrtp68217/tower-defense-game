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

    private LineRenderer _lineRenderer;
    private GameObject _gridObject;

    void Start()
    {
        ConfigureGridObject();
        ConfigureLineRenderer();
        CreateGrid();
    }


    private void ConfigureGridObject()
    {
        _gridObject = new GameObject("Grid");
        _gridObject.transform.SetParent(_terrain.transform);
        _gridObject.transform.localPosition = Vector3.zero;
    }

    private void ConfigureLineRenderer()
    {
        _lineRenderer = _gridObject.AddComponent<LineRenderer>();
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = _color;
        _lineRenderer.endColor = _color;
        _lineRenderer.startWidth = 0.15f;
        _lineRenderer.endWidth = 0.15f;
        _lineRenderer.useWorldSpace = true;
    }

    private void CreateGrid()
    {

        float terrainWidth = _terrain.terrainData.size.x;
        float terrainLength = _terrain.terrainData.size.z;

        int cellsX = Mathf.RoundToInt(terrainWidth / _cellSize);
        int cellsZ = Mathf.RoundToInt(terrainLength / _cellSize);

        int totalLines = (cellsX + 1) + (cellsZ + 1);
        int totalPositions = totalLines * 2;

        _lineRenderer.positionCount = totalPositions;
        Vector3[] positions = new Vector3[totalLines * 2];

        for (int i = 0; i <= cellsX; i++)
        {
            float x = i * _cellSize;

            CreateLine(
                new Vector3(x, _lineHeight, 0),
                new Vector3(x, _lineHeight, terrainLength)
            );
        }

        for (int i = 0; i <= cellsZ; i++)
        {
            float z = i * _cellSize;

            CreateLine(
                new Vector3(0, _lineHeight, z),
                new Vector3(terrainWidth, _lineHeight, z)
            );
        }

        _lineRenderer.SetPositions(positions);
    }

    private void CreateLine(Vector3 start, Vector3 end)
    {
        GameObject lineObject = new GameObject("GridLine");
        lineObject.transform.SetParent(_gridObject.transform);

        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = _color;
        lineRenderer.endColor = _color;
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}