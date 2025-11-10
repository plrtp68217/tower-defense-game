using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public abstract class TowerEntityBase : MonoBehaviour, IDamagable, IPlacable, IGridable, IDisposable
{
    [SerializeField] private TowerData _towerData;

    private Vector3 _worldPosition;

    public Vector2Int Size { get => _towerData.Size; }

    public TowerData TowerData { get => _towerData; }

    public Vector3 WorldPosition 
    {
        get => _worldPosition;
        set 
        {
            transform.position = value;
            _worldPosition = value;
        }
    }

    public Vector3Int GridPosition { get; set; }

    public Vector3 Center
    {
        get
        {
            return WorldPosition + new Vector3(Size.x * 0.5f, 0, Size.y * 0.5f);
        }
    }

    public IEnumerable<Vector3Int> OccupiedGridPositions
    {
        get
        {
            int width = Mathf.Max(0, Size.x);
            int height = Mathf.Max(0, Size.y);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    yield return GridPosition + new Vector3Int(x, 0, y);
                }
            }
        }
    }

    public virtual void TakeDamage(float damage, DamageSource source)
    {
        throw new NotImplementedException();
    }


    protected virtual void Die()
    {
        throw new NotImplementedException();
    }

    protected virtual void OnDamageTaken(float damage, DamageSource source)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        Destroy(gameObject);
    }
}
