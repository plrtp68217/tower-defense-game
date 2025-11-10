using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputService : MonoBehaviour
{
    [SerializeField]
    private Camera _sceneCamera;

    private Vector3 _lastPosition;

    [SerializeField]
    private LayerMask _placementLayerMask;

    public event Action OnClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }
    }

    public bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    /// <summary>
    /// Получает мировую позицию курсора мыши на поверхности размещения.
    /// Выполняет рейкаст из камеры через позицию мыши и возвращает точку пересечения.
    /// Использует маску слоя размещения для обнаружения допустимых поверхностей.
    /// </summary>
    /// <returns>Мировая позиция пересечения курсора мыши с поверхностью размещения</returns>
    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _sceneCamera.nearClipPlane;

        Ray ray = _sceneCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100, _placementLayerMask))
        {
            _lastPosition = hit.point;
        }

        return _lastPosition;
    }

    /// <summary>
    /// Получает мировую позицию курсора мыши на поверхности размещения и объект под курсором.
    /// Выполняет рейкаст из камеры через позицию мыши и возвращает точку пересечения и игровой объект.
    /// Использует маску слоя размещения для обнаружения допустимых поверхностей и объектов.
    /// </summary>
    /// <param name="selectedObject">Объект, находящийся под курсором мыши (null если объект не найден)</param>
    /// <returns>Мировая позиция пересечения курсора мыши с поверхностью размещения</returns>
    public Vector3 GetSelectedMapPosition(out GameObject selectedObject)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _sceneCamera.nearClipPlane;

        Ray ray = _sceneCamera.ScreenPointToRay(mousePosition);

        selectedObject = null;

        if (Physics.Raycast(ray, out RaycastHit hit, 100, _placementLayerMask))
        {
            _lastPosition = hit.point;
            selectedObject = hit.collider.gameObject;
        }

        return _lastPosition;
    }
}
