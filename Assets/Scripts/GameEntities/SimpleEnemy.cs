using System;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : DamagableEntity, IPlacable, IEnemy
{
    [field: SerializeField] public EntityData EntityData { get; set; }
    public Team Team { get; set; }
    public string Name { get; set; }
    public GameObject Prefab { get; set; }
    public Vector2Int Size { get; set; }
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
