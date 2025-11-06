using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Data/Unit Data", order = 1)]
public class UnitData : ScriptableObject
{
    [Header("Основные параметры")]
    public string EntityName;
    public GameObject Prefab;
    public Vector2Int Size;


    [Header("Характеристики")]
    public float MaxHealth = 100f;
    public float Damage = 1f;

    [Header("Дополнительные параметры")]
    public Team EntityTeam = Team.Blue;
}
