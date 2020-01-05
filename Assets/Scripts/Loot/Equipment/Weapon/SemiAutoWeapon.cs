using System.Collections;
using UnityEngine;

public class SemiAutoWeapon : Weapon
{
    private Stat burstCooldown;
    private int numShots;
    private float lastShotFired = 0;
    private bool isTriggerReleased;

    public SemiAutoWeapon() : base() { }

    public SemiAutoWeapon(SemiAutoWeaponConfig config, bool isDefault = false)
        : base(config, isDefault)
    {
        weaponType = WeaponType.SemiAuto;
        projectilePrefab = config.ProjectilePrefab;
        shotCooldown = config.ShotCooldown;
        burstCooldown = config.BurstCooldown;
        numShots = config.NumShots;
        isTriggerReleased = true;
    }

    public override void Activate()
    {
        if (IsShotReady() && isTriggerReleased)
        {
            isTriggerReleased = false;
            lastShotFired = Time.time;
            EquipmentManager.instance.StartCoroutine(FireShots());
        }
    }

    public override void Deactivate()
    {
        isTriggerReleased = true;
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
        projectile.SetDamage(damage.Value);
        Vector2 velocity = turret.transform.right * speed.Value;
        projectile.SetVelocity(velocity);

        return projectile;
    }

    private IEnumerator FireShots()
    {
        int shotsFired = 0;
        while (shotsFired < numShots)
        {
            CreateProjectile().Fire();
            PlayFireSFX();

            yield return new WaitForSeconds(shotCooldown.Value);
            shotsFired++;
        }

    }

    private bool IsShotReady()
    {
        bool isShotReady = false;
        if (Time.time - lastShotFired >= burstCooldown.Value)
        {
            isShotReady = true;
        }

        return isShotReady;
    }
}
