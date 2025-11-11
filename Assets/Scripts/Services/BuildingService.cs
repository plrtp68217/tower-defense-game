using UnityEngine;

public class BuildingService : MonoBehaviour
{
    [SerializeField] private PlacementMapService _map;

    public Vector3Int WorldToCell(Vector3 worldPosition) => _map.WorldToCell(worldPosition);
    public Vector3 CellToWorld(Vector3Int cellPosition) => _map.CellToWorld(cellPosition);

    public bool CanPlace(TowerEntityBase pos) => _map.CanPlaceObject(pos);

    public bool TryPlace(TowerEntityBase obj)
    {
        if (obj == null) return false;

        if (_map.CanPlaceObject(obj) == false)
        {
            return false;
        }

        _map.PlaceObject(obj);

        return true;
    }

    public T? GetObjectAtPosition<T>(Vector3Int position)
        where T : TowerEntityBase
    {
        return _map.GetObjectAtPosition(position) as T;
    }

    public bool RemoveObject(TowerEntityBase obj) => _map.RemoveObject(obj);

    public void ShowGrid()
    {
        _map.ShowGridVisualisation();
    }

    public void HideGrid()
    {
        _map.HideGridVisualisation();
    }
}
