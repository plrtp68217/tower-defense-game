using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private UnitData EnemyData;          
    [SerializeField] private Vector3Int SpawnPosition;       
    [SerializeField] private int EnemiesToSpawn = 2;     
    [SerializeField] private float SpawnInterval = 1f;   

    private EnemyFactory _factory;

    private void Start()
    {
        //_factory = new EnemyFactory();
        //StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < EnemiesToSpawn; i++)
        {
            SimpleUnit enemy = _factory.CreateEnemy(EnemyData, SpawnPosition);
            //Сдвигаем
            SpawnPosition.Set(SpawnPosition.x + 1, SpawnPosition.y, SpawnPosition.z + 1);

            if (enemy != null)
            {
                Debug.Log($"Создан враг: {enemy.Name}, HP: {enemy.Health}");
            }

            yield return new WaitForSeconds(SpawnInterval);
        }
    }
}
