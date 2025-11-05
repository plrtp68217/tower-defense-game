using UnityEngine;

public class EnemyFactory
{
    public SimpleEnemy CreateEnemy(EntityData data, Vector3Int position)
    {
        GameObject enemyObj = Object.Instantiate(data.Prefab, position, Quaternion.identity);
        
        if (!enemyObj.TryGetComponent<SimpleEnemy>(out var enemy))
        {
            Debug.LogError("Префаб врага не содержит компонент SimpleEnemy!");
            Object.Destroy(enemyObj);
            return null;
        }

        // 3. Настраиваем данные из EntityData
        enemy.EntityData = data;
        enemy.Name = data.EntityName;
        enemy.Health = data.MaxHealth;
        enemy.Prefab = data.Prefab;
        enemy.Size = data.Size;
        enemy.Team = data.EntityTeam;
        enemy.Position = position;


        return enemy;
    }
}
