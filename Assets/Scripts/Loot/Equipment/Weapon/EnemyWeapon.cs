public class EnemyWeapon : AutoWeapon
{
    private float cooldownVariation;

    public float GetCooldownVariation() => cooldownVariation;

    public EnemyWeapon() : base() { }

    public EnemyWeapon(EnemyWeaponConfig config) : base(config)
    {
        cooldownVariation = config.CooldownVariation;
    }
}
