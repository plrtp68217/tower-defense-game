using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Data/TowerData", order = 1)]
public class TowerData : ScriptableObject
{
    public string Name = "Tower";
    public GameObject UnitPrefab;
    public Material PreviewMaterial;
    public Vector2Int Size;
}