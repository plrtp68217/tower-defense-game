using UnityEngine;

public interface IPlacable
{
    public Vector2Int Size { get; }
    public GameObject Prefab { get; }
}
