using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public sealed class PlacementMapService : MonoBehaviour
{
    [SerializeField] private Grid _grid;

    [SerializeField] private GameObject _gridVisualisation;

    private List<IGridable> _placedObjects;

    private void Awake()
    {
        _placedObjects = new List<IGridable>();
    }

    public Vector3Int WorldToCell(Vector3 worldPosition)
    {
        return _grid.WorldToCell(worldPosition);
    }

    public Vector3 CellToWorld(Vector3Int cellPosition)
    {
        return _grid.CellToWorld(cellPosition);
    }

    public void PlaceObject(IGridable obj)
    {
        if (obj == null)
            return;

        _placedObjects.Add(obj);
    }

    public bool CanPlaceObject(IGridable obj)
    {
        if (obj == null)
            return false;

        var objPositions = obj.OccupiedGridPositions;
        var occupiedSet = new HashSet<Vector3Int>(_placedObjects.SelectMany(o => o.OccupiedGridPositions));

        return !objPositions.Any(pos => occupiedSet.Contains(pos));
    }

    public bool RemoveObject(IGridable obj)
    {
        if (obj == null || !obj.OccupiedGridPositions.Any())
            return false;

        bool removed = _placedObjects.Remove(obj);

        if (!removed)
            throw new InvalidOperationException(
                "Ошибка: объект удалён частично. Несогласованное состояние сетки!");

        return removed;
    }

    public void Clear()
    {
        _placedObjects.Clear();
    }

    public IGridable GetObjectAtPosition(Vector3Int position)
    {
        foreach (IGridable obj in _placedObjects)
        {
            foreach (Vector3Int objPosition in obj.OccupiedGridPositions)
            {
                if (objPosition == position) return obj;
            }
        }

        return null;
    }

    public IEnumerable<Vector3Int> GetAllOccupiedPositions()
    {
        return _placedObjects.SelectMany(x => x.OccupiedGridPositions);
    }

    public void ShowGridVisualisation()
    {
        _gridVisualisation.SetActive(true);
    }

    public void HideGridVisualisation()
    {
        _gridVisualisation.SetActive(false);
    }
}