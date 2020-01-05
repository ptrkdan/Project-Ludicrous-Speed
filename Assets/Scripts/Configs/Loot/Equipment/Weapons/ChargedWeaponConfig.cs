using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Equipment/Charged Weapon")]
public class ChargedWeaponConfig : WeaponConfig
{
    [SerializeField] private ChargedProjectile projectilePrefab;
    [SerializeField] private Stat chargeDuration;

    public ChargedProjectile ProjectilePrefab { get => projectilePrefab; set => projectilePrefab = value; }
    public Stat ChargeDuration { get => chargeDuration; set => chargeDuration = value; }

    public override Loot Create(bool isDefault)
    {
        return new ChargedWeapon(this, isDefault);
    }

    public override Loot Create()
    {
        return new ChargedWeapon(this);
    }
}
