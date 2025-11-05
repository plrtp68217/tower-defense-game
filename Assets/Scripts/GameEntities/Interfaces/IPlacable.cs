using System.Collections.Generic;
using UnityEngine;


public interface IPlacable
{
    Vector3Int Position { get; set; }
    Vector2Int Size { get; }
    GameObject Prefab { get; }


    /// <summary>
    /// Автовычисляемая коллекция занятых позиций (должна рассчитывается на лету в зависимости от позиции и размера).
    /// </summary>
    IEnumerable<Vector3Int> OccupiedPositions { get; }
}
