using UnityEngine;

public class BuildingService : MonoBehaviour
{
    [SerializeField] private PlacementMapService _map;

    public Vector3Int WorldToCell(Vector3 worldPosition) => _map.WorldToCell(worldPosition);
    public Vector3 CellToWorld(Vector3Int cellPosition) => _map.CellToWorld(cellPosition);

    public bool CanPlace(IPlacable obj) => _map.CanPlaceObject(obj);

    public bool TryPlace(IPlacable obj)
    {
        if (obj == null) return false;

        if (!_map.TryPlaceObject(obj))
            return false;

        GameObject newObject = Instantiate(obj.Prefab);
        newObject.transform.position = _map.CellToWorld(obj.Position);

        return true;
    }
    public bool RemoveObject(IPlacable obj) => _map.RemoveObject(obj);

    public void ShowGrid()
    {
        _map.ShowGridVisualisation();
    }

    public void HideGrid()
    {
        _map.HideGridVisualisation();
    }
}
