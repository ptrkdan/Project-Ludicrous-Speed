public class EnemyWeapon : Weapon
{
    float cooldownVariation;

    public float GetCoolDownVariation() => cooldownVariation;

    public EnemyWeapon() : base() { }

    public EnemyWeapon(EnemyWeaponConfig config) : base(config)
    {
        cooldownVariation = config.CooldownVariation;
    }
}
