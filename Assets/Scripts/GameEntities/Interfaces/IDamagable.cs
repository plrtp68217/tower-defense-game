public interface IDamagable
{
    void TakeDamage(int damage, Team team);
    void DealDamage(IDamagable damageTarget);
}

