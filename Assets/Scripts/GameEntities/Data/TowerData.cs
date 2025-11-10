using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Data/Tower Data", order = 1)]
public class TowerData : ScriptableObject
{
    [Header("Основные параметры")]
    public string Name = "Tower";
    public Team EntityTeam = Team.Blue;
    public int Level = 1;

    [Header("Юниты")]
    public GameObject UnitPrefab;
    public int UnitsPerLevel = 10;


    [Header("Дополнительные параметры")]
    public Material PreviewMaterial;
    public Vector2Int Size;
}
