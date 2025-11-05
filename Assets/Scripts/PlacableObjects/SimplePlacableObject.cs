using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Placement/PlacableObject", fileName = "PlacableObject", order = 1)]
public class SimplePlacableObject : ScriptableObject, IPlacable
{
    [Tooltip("Префаб объекта, который будет размещаться")]
    [SerializeField]
    private GameObject _prefab;

    [Tooltip("Размер в клетках сетки")]
    [SerializeField]
    private Vector2Int _size = Vector2Int.one;

    public GameObject Prefab => _prefab;
    public Vector2Int Size => _size;

    public Vector3Int Position { get; set; }

    public IEnumerable<Vector3Int> OccupiedPositions
    {
        get
        {
            int width = Mathf.Max(0, Size.x);
            int height = Mathf.Max(0, Size.y);

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    yield return Position + new Vector3Int(x, 0, y);
        }
    }
}
