using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerEntityBase : IDamagable, IPlacable, IGridable, IDisposable
{
    public Vector2Int Size { get; set; }
    public GameObject Instance { get; set; }
    public GameObject Prefab { get; set; }
    public float Health { get; set; }
    public bool IsAlive => Health > 0;

    private Vector3 _wordPosition;
    public Vector3 WorldPosition 
    {
        get => _wordPosition;
        set 
        {
            Instance.transform.position = _wordPosition;
            _wordPosition = value;
        }
    }

    public Vector3Int GridPosition { get; set; }

    public Vector3 Center
    {
        get
        {
            return WorldPosition + new Vector3(Size.x / 2f, 0, Size.y / 2f);
        }
    }

    public IEnumerable<Vector3Int> OccupiedGridPositions
    {
        get
        {
            int width = Mathf.Max(0, Size.x);
            int height = Mathf.Max(0, Size.y);

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    yield return GridPosition + new Vector3Int(x, 0, y);
        }
    }
    public virtual void TakeDamage(float damage, DamageSource source)
    {
        if (!IsAlive) return;

        Health -= damage;

        if (Health <= 0)
        {
            Die();
        }
        else
        {
            OnDamageTaken(damage, source);
        }
    }


    protected virtual void Die()
    {
        // Общая анимация смерти
        //UnityEngine.Object.Destroy(Prefab);
    }

    protected virtual void OnDamageTaken(float damage, DamageSource source)
    {
        // Можно переопределить в наследниках
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(Instance);
    }
}
