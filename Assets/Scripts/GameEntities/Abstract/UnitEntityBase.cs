using System;
using UnityEngine;

public abstract class UnitEntityBase : MonoBehaviour,  IDamagable, IPlacable
{
    [field: SerializeField] public UnitData UnitData { get; set; }

    public bool IsAlive => UnitData.Health > 0;

    public Vector3 WorldPosition { get; set; }

    public virtual void TakeDamage(int damage, DamageSource source)
    {
        if (!IsAlive) return;

        UnitData.Health -= damage;

        if (UnitData.Health <= 0)
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
        throw new NotImplementedException();
    }

    protected virtual void OnDamageTaken(float damage, DamageSource source)
    {
        throw new NotImplementedException();
    }
}
