using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class UnitEntityBase : MonoBehaviour,  IDamagable, IPlacable
{
    [field: SerializeField] public UnitData UnitData { get; set; }

    public bool IsAlive => UnitData.Health > 0;

    public Vector3 WorldPosition { get; set; }

    public void DealDamage(IDamagable damageTarget)
    {
        damageTarget.TakeDamage(UnitData.Damage, UnitData.Team);
    }

    public void TakeDamage(int damage, Team team)
    {
        throw new NotImplementedException();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        UnitData = UnitData.Clone();
    }
}
