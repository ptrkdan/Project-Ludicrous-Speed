using UnityEngine;

public class AutoWeapon : Weapon
{

    public AutoWeapon() : base() { }

    public AutoWeapon(AutoWeaponConfig config, bool isDefault = false)
        : base(config, isDefault)
    {
        weaponType = WeaponType.Auto;
        projectilePrefab = config.ProjectilePrefab;
        cooldown = config.Cooldown;
    }

    public override void Activate(Vector3 weaponPosition, Quaternion weaponRotation)
    {
        base.Activate(weaponPosition, weaponRotation);
        Projectile projectile = Instantiate(
                projectilePrefab,
                weaponPosition,
                weaponRotation);
        projectile.SetDamage((int)damage.GetCalcValue());
        projectile.SetSpeed(speed.GetCalcValue());
        projectile.Fire();
        PlayFireSFX();
    }
}
