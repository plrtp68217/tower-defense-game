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
}
