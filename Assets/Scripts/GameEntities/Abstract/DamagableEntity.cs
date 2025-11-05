using UnityEngine;

public abstract class DamagableEntity : MonoBehaviour, IDamagable
{
    public float Health { get; set; }
    public bool IsAlive => Health > 0;

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
        Destroy(gameObject);
    }

    protected virtual void OnDamageTaken(float damage, DamageSource source)
    {
        // Можно переопределить в наследниках
    }
}
