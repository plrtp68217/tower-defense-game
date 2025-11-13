using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Data/Tower Data", order = 1)]
public class TowerData : ScriptableObject
{
    [Header("Общие")]
    public string Name = "Tower";
    public GameObject UnitPrefab;
    public int UnitsPerLevel = 10;
    public Material PreviewMaterial;
    public Vector2Int Size;

    [Header("Частные")]
    public Team Team = Team.Blue;
    public int Level = 1;
    public int UnitsCount = 0;
}
