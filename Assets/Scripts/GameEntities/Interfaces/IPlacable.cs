using System.Collections.Generic;
using UnityEngine;


public interface IPlacable
{
    Vector3 WordPosition { get; set; }
    GameObject Prefab { get; }
    Vector2Int Size { get; }
}
