using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    private readonly Dictionary<Vector3Int, PlacementData> _placedObjects = new();

    public void AddObject(Vector3Int gridPosition, Vector2Int objectSize, int ID, int objectIndex)
    {
        List<Vector3Int> positionsToOccupy = CalculatePositions(gridPosition, objectSize);

        PlacementData data = new(positionsToOccupy, ID, objectIndex);

        bool placementValidity = CanPlaceObject(gridPosition, objectSize);

        if (placementValidity == false)
        {
            return;
        }

        foreach (var position in positionsToOccupy)
        {
            _placedObjects[position] = data;
        }
    }

    public bool CanPlaceObject(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> postionsToOccupy = CalculatePositions(gridPosition, objectSize);

        foreach (var position in postionsToOccupy)
        {
            if (_placedObjects.ContainsKey(position))
            {
                return false;
            }
        }

        return true;
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnValue = new();

        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnValue.Add(gridPosition + new Vector3Int(x, 0, y));
            }
        }

        return returnValue;
    }
}

public class PlacementData
{
    public List<Vector3Int> occupiedPositions;

    public int ID { get; private set; }

    public int PlacedObjectIndex { get; private set; }

    public PlacementData(List<Vector3Int> occupiedPositions, int ID, int placedObjectIndex)
    {
        this.occupiedPositions = occupiedPositions;
        this.ID = ID;
        PlacedObjectIndex = placedObjectIndex;
    }
}
