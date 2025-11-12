using UnityEngine;

public class Billboarding: MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    private void Update()
    {
        Quaternion rotation = _mainCamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }
}