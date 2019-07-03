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


    public override void Activate()
    {
        Projectile projectile = CreateProjectile();
        projectile.Fire();
        PlayFireSFX();
    }

    public override void SetTurretPosition(Transform turret)
    {
        this.turret = turret;
    }

    protected override Projectile CreateProjectile()
    {
        Projectile projectile = Instantiate(
                projectilePrefab,
                turret.position,
                turret.rotation);
        projectile.SetDamage(damage.GetCalcValue());
        projectile.SetSpeed(speed.GetCalcValue());

        return projectile;
    }
}
