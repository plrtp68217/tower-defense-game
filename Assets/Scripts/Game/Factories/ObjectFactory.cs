using UnityEngine;

public class ObjectFactory : MonoBehaviour, IObjectFactory
{
    public GameObject Instantiate(GameObject prefab)
    {
        return UnityEngine.Object.Instantiate(prefab);
    }

    public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return UnityEngine.Object.Instantiate(prefab, position, rotation);
    }
}