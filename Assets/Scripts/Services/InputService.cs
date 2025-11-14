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
    private readonly float _rayCastDistance = 100f; 

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
        Vector3 screenPoint = GetScreenPoint();
        Ray ray = _sceneCamera.ScreenPointToRay(screenPoint);

        if (Physics.Raycast(ray, out RaycastHit hit, _rayCastDistance, _placementLayerMask))
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

    public void GetObjectInMap(int layerNumber, out GameObject obj)
    {
        obj = null;

        Vector3 screenPoint = GetScreenPoint();
        Ray ray = _sceneCamera.ScreenPointToRay(screenPoint);

        int layerMask = 1 << layerNumber;

        if (Physics.Raycast(ray, out RaycastHit hit, _rayCastDistance, layerMask))
        {
            obj = hit.collider.gameObject;
        }
    }

    private Vector3 GetScreenPoint()
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = _sceneCamera.nearClipPlane;

        return screenPoint;
    }
}