using System.Collections.Generic;
using UnityEngine;

public interface IGridable
{

    Vector3Int GridPosition { get; set; }

    /// <summary>
    /// Автовычисляемая коллекция занятых позиций (должна рассчитывается на лету в зависимости от позиции и размера).
    /// </summary>
    IEnumerable<Vector3Int> OccupiedGridPositions { get; }
}
