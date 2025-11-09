using System.Collections.Generic;
using UnityEngine;


public interface IPlacable
{
    Vector3 WorldPosition { get; set; }
    Vector2Int Size { get; }
}
