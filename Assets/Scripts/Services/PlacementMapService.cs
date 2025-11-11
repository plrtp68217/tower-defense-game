using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


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

    [SerializeField] public GameObject _gridVisualisation;

    private List<IGridable> _placedObjects;

    private void Awake()
    {
        _placedObjects = new List<IGridable>();
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
    public void PlaceObject(IGridable obj)
    {
        if (obj == null)
            return;

        _placedObjects.Add(obj);
    }

    /// <summary>
    /// Проверяет, можно ли разместить объект на сетке (без фактического размещения).
    /// </summary>
    /// <param name="obj">Объект с заданными Position и Size</param>
    /// <returns>true, если место свободно; false при коллизии или null-объекте</returns>
    public bool CanPlaceObject(IGridable obj)
    {
        if (obj == null)
            return false;

        var objPositions = obj.OccupiedGridPositions;
        var occupiedSet = new HashSet<Vector3Int>(_placedObjects.SelectMany(o => o.OccupiedGridPositions));

        return !objPositions.Any(pos => occupiedSet.Contains(pos));
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
    public bool RemoveObject(IGridable obj)
    {
        if (obj == null || !obj.OccupiedGridPositions.Any())
            return false;

        //bool allRemoved = obj.OccupiedGridPositions.All(pos => _placedObjects.Remove(pos));

        bool removed = _placedObjects.Remove(obj);

        if (!removed)
            throw new InvalidOperationException(
                "Ошибка: объект удалён частично. Несогласованное состояние сетки!");

        return removed;
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
    //public bool IsPositionOccupied(Vector3Int position)
    //{
    //    return _placedObjects.ContainsKey(position);
    //}

    /// <summary>
    /// Получает объект, занимающий указанную позицию (если есть).
    /// </summary>
    /// <param name="position">Координаты сетки</param>
    /// <returns>Объект типа IGridable или null, если позиция свободна</returns>
    public IGridable? GetObjectAtPosition(Vector3Int position)
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

    /// <summary>
    /// Возвращает все текущие занятые позиции на сетке.
    /// </summary>
    /// <returns>Коллекция Vector3Int с координатами занятых ячеек</returns>
    public IEnumerable<Vector3Int> GetAllOccupiedPositions()
    {
        return _placedObjects.SelectMany(x => x.OccupiedGridPositions);
    }

    /// <summary>
    /// Отображает сетку.
    /// </summary>
    public void ShowGridVisualisation()
    {
        _gridVisualisation.SetActive(true);
    }

    /// <summary>
    /// Скрывает сетку.
    /// </summary>
    public void HideGridVisualisation()
    {
        _gridVisualisation.SetActive(false);
    }
}
