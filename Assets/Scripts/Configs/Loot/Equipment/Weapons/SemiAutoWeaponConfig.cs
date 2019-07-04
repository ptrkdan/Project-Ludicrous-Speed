using UnityEngine;

[CreateAssetMenu(menuName ="Configs/Equipment/Semi Auto Weapon")]
public class SemiAutoWeaponConfig : WeaponConfig
{
    [Header("Projectile")]
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Stat burstCooldown;
    [SerializeField] int numOfShots;

    public Projectile ProjectilePrefab { get => projectilePrefab; set => projectilePrefab = value; }
    public Stat BurstCooldown { get => burstCooldown; set => burstCooldown = value; }
    public int NumShots { get => numOfShots; set => numOfShots = value; }

    public override Loot Create(bool isDefault)
    {
        return new SemiAutoWeapon(this, isDefault);
    }

    public override Loot Create()
    {
        return new SemiAutoWeapon(this);
    }


}
