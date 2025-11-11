using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputService : MonoBehaviour
{
    [SerializeField]
    private Camera _sceneCamera;

    private Vector3 _lastPosition;

    [SerializeField]
    private LayerMask _placementLayerMask;

    private readonly Collider[] _collidersBuffer = new Collider[32];

    public event Action OnClicked, OnPressed;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            OnPressed?.Invoke();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnClicked?.Invoke();
        }
    }

    public bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public Vector3 GetSelectedMapPosition() => GetSelectedMapPosition(out var _);

    public Vector3 GetSelectedMapPosition(out GameObject selectedObject)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _sceneCamera.nearClipPlane;

        Ray ray = _sceneCamera.ScreenPointToRay(mousePosition);

        selectedObject = null;

        if (Physics.Raycast(ray, out RaycastHit hit, 100, _placementLayerMask))
        {
            _lastPosition = hit.point;

            int layerMask = 1 << LayerMask.NameToLayer(Layers.Line);

            var n = Physics.OverlapSphereNonAlloc(_lastPosition, 0.5f, _collidersBuffer, layerMask);

            Debug.Log(n);

            if (n > 0) selectedObject = _collidersBuffer.First().gameObject;
        }

        return _lastPosition;
    }
}