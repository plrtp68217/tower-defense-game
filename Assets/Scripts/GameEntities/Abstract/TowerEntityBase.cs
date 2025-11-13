using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerEntityBase : MonoBehaviour, IDamagable, IPlacable, IGridable, IDisposable
{
    [field: SerializeField] public TowerData TowerData { get; set; }

    private Vector3 _worldPosition;

    public Vector2Int Size { get => TowerData.Size; }

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

    public void Dispose()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage, Team team)
    {
        int damageModifier = team == TowerData.Team ? 1 : -1;

        TowerData.UnitsCount += damage * damageModifier;

        if (TowerData.UnitsCount < 0)
        {
            TowerData.Team = team;
            TowerData.UnitsCount = 0;
        }
        else if (TowerData.UnitsCount == TowerData.UnitsPerLevel) {
            TowerData.Level += 1;
            TowerData.UnitsCount = 0;
        }
    }

    public void DealDamage(IDamagable damageTarget)
    {
        throw new NotImplementedException();
    }

    private void Awake()
    {
        TowerData = TowerData.Clone();
    }
}
