using UnityEngine;

public abstract class UnitEntityBase :  IDamagable, IPlacable
{
    public Vector2Int Size { get; set; }
    public GameObject Instance { get; set; }
    public GameObject Prefab { get; set; }
    public float Health { get; set; }
    public bool IsAlive => Health > 0;

    public Vector3 WorldPosition { get; set; }

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
}
