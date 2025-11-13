using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class UnitEntityBase : MonoBehaviour,  IDamagable, IPlacable
{
    [SerializeField] protected UnitData _data;

    public Team Team { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }

    public Vector3 WorldPosition { get; set; }

    public void DealDamage(IDamagable damageTarget)
    {
        damageTarget.TakeDamage(Damage, Team);
    }

    public void TakeDamage(int damage, Team team)
    {
        if (Team == team) return;

        Health -= damage;

        if (Health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        Team = _data.Team;
        Health = _data.Health;
        Damage = _data.Damage;
    }
}
