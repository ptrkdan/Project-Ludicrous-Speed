using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Enemy Weapon Config")]
public class EnemyWeaponConfig : WeaponConfig 
{
    [Header("Misc.")]
    [SerializeField] float cooldownVariation;

    public float CooldownVariation { get => cooldownVariation; set => cooldownVariation = value; }

    public override Loot Create()
    {
        return new EnemyWeapon(this);
    }
}

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
