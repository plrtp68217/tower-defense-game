public interface IDamagable
{
    float Health { get; }
    bool IsAlive { get; }
    void TakeDamage(float damage , DamageSource damageSource);
}

