using System.Collections.Generic;
using UnityEngine;

public abstract class TowerEntityBase : UnitEntityBase, IGridable
{
    public Vector3Int GridPosition { get; set; }

    public IEnumerable<Vector3Int> OccupiedGridPositions
    {
        get
        {
            int width = Mathf.Max(0, Size.x);
            int height = Mathf.Max(0, Size.y);

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    yield return GridPosition + new Vector3Int(x, 0, y);
        }
    }
}
