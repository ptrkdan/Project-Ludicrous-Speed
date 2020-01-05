using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstWeapon : Weapon
{
    private new Projectile projectilePrefab;
    private float burstRadius;
    private int numOfShots;
    private Quaternion originalTurretRotation;
    private float lastShotFired;
    private bool isTriggerReleased;

    public BurstWeapon() : base() { }

    public BurstWeapon(BurstWeaponConfig config, bool isDefault = false)
        : base(config, isDefault)
    {
        projectilePrefab = config.ProjectilePrefab;
        burstRadius = config.BurstRadius;
        numOfShots = config.NumOfShots;

        lastShotFired = 0;
        isTriggerReleased = true;
    }

    public override void Activate()
    {
        if (IsShotReady() && isTriggerReleased)
        {
            isTriggerReleased = false;
            lastShotFired = Time.time;
            for (int i = 0; i < numOfShots; i++)
            {
                CreateProjectile().Fire();
                ResetTurretRotation();
            }
        }
    }

    public override void Deactivate()
    {
        isTriggerReleased = true;
    }

    public override void SetTurretPosition(Transform turret)
    {
        this.turret = turret;
        originalTurretRotation = turret.rotation;
    }

    protected override Projectile CreateProjectile()
    {
        // Calc random angle
        Vector3 randomEulerAngle = new Vector3(0, 0, Random.Range(-burstRadius, burstRadius));
        Quaternion randomRotation = Quaternion.Euler(randomEulerAngle);
        turret.rotation *= randomRotation;

        Projectile projectile = Instantiate(
                projectilePrefab,
                turret.position,
                turret.rotation);  // Add random rotation to turret rotation
        projectile.SetDamage(damage.Value);
        Vector2 velocity = turret.transform.right * speed.Value;
        projectile.SetVelocity(velocity);

        return projectile;
    }

    private bool IsShotReady()
    {
        bool isShotReady = false;

        if (Time.time - lastShotFired >= shotCooldown.Value)
        {
            isShotReady = true;
        }

        return isShotReady;
    }

    private void ResetTurretRotation()
    {
        turret.rotation = originalTurretRotation;
    }
}
