public struct DamageSource
{
    public IUnit Attacker { get; set; }
    public bool IsCritical { get; set; }
    public float BaseDamage { get; set; }
    public float DamageMultiplier { get; set; }

    public readonly float FinalDamage 
    {
        get => (BaseDamage + (BaseDamage * DamageMultiplier)) * (IsCritical ? 2f : 1f); 
    }
}
