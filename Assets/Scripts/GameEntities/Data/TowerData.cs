using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Data/Tower Data", order = 1)]
public class TowerData : ScriptableObject
{
    [Header("Основные параметры")]
    public string Name = "Tower";
    public Team Team = Team.Blue;
    public int Level = 1;

    [Header("Юниты")]
    public GameObject UnitPrefab;
    public int UnitsPerLevel = 10;
    public int UnitsCount = 0;

    [Header("Дополнительные параметры")]
    public Material PreviewMaterial;
    public Vector2Int Size;

    public TowerData Clone()
    {
        TowerData clone = CreateInstance<TowerData>();
        clone.Name = Name;
        clone.Team = Team;
        clone.Level = Level;
        clone.UnitPrefab = UnitPrefab;
        clone.UnitsPerLevel = UnitsPerLevel;
        clone.UnitsCount = UnitsCount;
        clone.PreviewMaterial = PreviewMaterial;
        clone.Size = Size;
        return clone;
    }
}
