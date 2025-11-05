using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Enemy Data", order = 1)]
public class EntityData : ScriptableObject
{
    [Header("Основные параметры")]
    public string EntityName;
    public GameObject Prefab;
    public Vector2Int Size;


    [Header("Характеристики")]
    public float MaxHealth = 100f;
    public float Damage = 1f;

    [Header("Дополнительные параметры")]
    public Team EntityTeam = Team.Enemy;
}
