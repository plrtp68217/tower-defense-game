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

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _sceneCamera.nearClipPlane;

        Ray ray = _sceneCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, _placementLayerMask))
        {
            _lastPosition =  hit.point;
        }

        return _lastPosition;
    }

    public GameObject GetObjectInRadius(Vector3 center, float radius, int layerNumber)
    {
        int layerMask = 1 << layerNumber;

        int hitCount = Physics.OverlapSphereNonAlloc(center, radius, _collidersBuffer, layerMask);

        if (hitCount > 0)
        {
            return _collidersBuffer.First().gameObject;
        }

        return null;
    }
}