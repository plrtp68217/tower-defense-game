using UnityEngine;

public class EnemyFactory
{
    public SimpleUnit CreateEnemy(UnitData data, Vector3Int position)
    {
        var obj = Object.Instantiate(data.Prefab);
        obj.transform.position = position;

        var unit = new SimpleUnit
        {
            Data = data,
            Prefab = data.Prefab,
            Name = data.EntityName,
            Health = data.MaxHealth,
            Size = data.Size,
            Team = data.EntityTeam,
            WordPosition = position
        };

        return unit;
    }
}
