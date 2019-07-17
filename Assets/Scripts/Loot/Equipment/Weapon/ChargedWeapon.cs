using UnityEngine;

public class ChargedWeapon : Weapon
{
    Stat chargeDuration;
    ChargedProjectile chargingProjectile;

    public ChargedWeapon() : base() { }

    public ChargedWeapon(ChargedWeaponConfig config, bool isDefault = false)
        : base(config, isDefault)
    {
        weaponType = WeaponType.Charged;
        projectilePrefab = config.ProjectilePrefab;
        chargeDuration = config.ChargeDuration;
    }

    public override void Activate()
    {
        if (!chargingProjectile)
        {
            chargingProjectile = CreateProjectile() as ChargedProjectile;
        }

        // Keep projectile attached to ship
        chargingProjectile.transform.position = turret.position;
    }

    public override void Deactivate()
    {
        if (chargingProjectile.IsCharged)
        {
            chargingProjectile.EnableCollider();
            chargingProjectile.Fire();
            chargingProjectile = null;
            PlayFireSFX();
        }
        else
        {
            DestroyProjectile();
        }
    }

    public override void SetTurretPosition(Transform turret)
    {
        this.turret = turret;
    }

    protected override Projectile CreateProjectile()
    {
        ChargedProjectile projectile = Instantiate(
                projectilePrefab,
                turret.position,
                turret.rotation) as ChargedProjectile;
        projectile.SetDamage(damage.GetCalcValue());
        Vector2 velocity = turret.transform.right * speed.GetCalcValue();
        projectile.SetVelocity(velocity);

        return projectile;
    }

    private void DestroyProjectile()
    {
        // TODO: Play decharge animation;

        Destroy(chargingProjectile.gameObject);
    }
}
