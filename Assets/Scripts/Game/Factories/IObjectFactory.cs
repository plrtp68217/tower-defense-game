using UnityEngine;

public interface IObjectFactory
{
    GameObject Instantiate(GameObject prefab);
    GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation);
}
