using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera _sceneCamera;

    private Vector3 _lastPosition;

    [SerializeField]
    private LayerMask _placementLayerMask;

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _sceneCamera.nearClipPlane;

        Ray ray = _sceneCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, _placementLayerMask))
        {
            _lastPosition = hit.point;
        }

        return _lastPosition;
    }
}
