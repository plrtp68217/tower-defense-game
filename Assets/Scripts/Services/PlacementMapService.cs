using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Отвечает за:
/// - преобразование координат (мир <-> сетка)
/// - проверку коллизий
/// - размещение/удаление объектов
/// - контроль занятых позиций
/// </summary>
public sealed class PlacementMapService : MonoBehaviour
{
    [SerializeField] private Grid _grid;

    private Dictionary<Vector3Int, IPlacable> _placedObjects;

    private void Awake()
    {
        _placedObjects = new Dictionary<Vector3Int, IPlacable>();
    }

    /// <summary>
    /// Преобразует мировую позицию в координаты сетки.
    /// </summary>
    /// <param name="worldPosition">Позиция в мировом пространстве</param>
    /// <returns>Координаты сетки (Vector3Int)</returns>
    public Vector3Int WorldToCell(Vector3 worldPosition)
    {
        return _grid.WorldToCell(worldPosition);
    }

    /// <summary>
    /// Преобразует координаты сетки в мировую позицию.
    /// </summary>
    /// <param name="cellPosition">Координаты сетки</param>
    /// <returns>Позиция в мировом пространстве (Vector3)</returns>
    public Vector3 CellToWorld(Vector3Int cellPosition)
    {
        return _grid.CellToWorld(cellPosition);
    }

    /// <summary>
    /// Пытается разместить объект на сетке.
    /// </summary>
    /// <param name="obj">Объект для размещения</param>
    /// <returns>true, если размещение успешно; false при коллизии или ошибке</returns>
    public bool TryPlaceObject(IPlacable obj)
    {
        if (obj == null)
            return false;

        // Проверяем, что все позиции, которые займёт объект, свободны
        var canPlace = obj.OccupiedPositions.All(pos => !_placedObjects.ContainsKey(pos));
        if (!canPlace)
            return false;

        // Размещаем объект: добавляем все его позиции в словарь
        foreach (var pos in obj.OccupiedPositions)
        {
            _placedObjects[pos] = obj;
        }

        return true;
    }

    /// <summary>
    /// Проверяет, можно ли разместить объект на сетке (без фактического размещения).
    /// </summary>
    /// <param name="obj">Объект с заданными Position и Size</param>
    /// <returns>true, если место свободно; false при коллизии или null-объекте</returns>
    public bool CanPlaceObject(IPlacable obj)
    {
        //if (obj.OccupiedPositions is null) throw new NullReferenceException(nameof(obj.OccupiedPositions));
        //if (obj is null) throw new NullReferenceException(nameof(obj));
        //if (_placedObjects is null) throw new NullReferenceException(nameof(_placedObjects));

        if (obj == null)
            return false;

        var positions = obj.OccupiedPositions;

        return positions.All(pos => !_placedObjects.ContainsKey(pos));
    }


    /// <summary>
    /// Удаляет объект из сетки (освобождает все занятые им позиции).
    /// </summary>
    /// <param name="obj">Объект для удаления</param>
    /// <returns>
    /// true — если объект успешно удалён;  
    /// false — если объект null или не занимал позиций
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Выбрасывается, если объект удалён частично (несогласованное состояние)
    /// </exception>
    public bool RemoveObject(IPlacable obj)
    {
        if (obj == null || !obj.OccupiedPositions.Any())
            return false;

        bool allRemoved = obj.OccupiedPositions.All(pos => _placedObjects.Remove(pos));

        if (!allRemoved)
            throw new InvalidOperationException(
                "Ошибка: объект удалён частично. Несогласованное состояние сетки!");

        return allRemoved;
    }

    /// <summary>
    /// Полностью очищает сетку от всех размещённых объектов.
    /// </summary>
    public void Clear()
    {
        _placedObjects.Clear();
    }

    /// <summary>
    /// Проверяет, занята ли конкретная позиция сетки.
    /// </summary>
    /// <param name="position">Координаты сетки</param>
    /// <returns>true, если позиция занята; false — свободна</returns>
    public bool IsPositionOccupied(Vector3Int position)
    {
        return _placedObjects.ContainsKey(position);
    }

    /// <summary>
    /// Получает объект, занимающий указанную позицию (если есть).
    /// </summary>
    /// <param name="position">Координаты сетки</param>
    /// <returns>Объект типа IPlacable или null, если позиция свободна</returns>
    public IPlacable GetObjectAtPosition(Vector3Int position)
    {
        return _placedObjects.TryGetValue(position, out var obj) ? obj : null;
    }

    /// <summary>
    /// Возвращает все текущие занятые позиции на сетке.
    /// </summary>
    /// <returns>Коллекция Vector3Int с координатами занятых ячеек</returns>
    public IEnumerable<Vector3Int> GetAllOccupiedPositions()
    {
        return _placedObjects.Keys;
    }
}
