using UnityEngine;



[CreateAssetMenu(fileName = "TowerData", menuName = "Data/Tower Data", order = 1)]
public class TowerData : ScriptableObject
{
    [Header("Основные параметры")]
    public string EntityName;
    public Material PreviewMaterial;
    public Vector2Int Size;


    [Header("Характеристики")]
    public float MaxHealth = 100f;
    public float Damage = 1f;

    [Header("Дополнительные параметры")]
    public Team EntityTeam = Team.Blue;
}
