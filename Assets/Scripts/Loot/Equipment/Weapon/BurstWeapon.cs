using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstWeapon : Weapon
{
    Projectile projectilePrefab;
    float burstRadius;
    int numOfShots;

    Quaternion originalTurretRotation;
    float lastShotFired;
    bool isKeyReleased;

    public BurstWeapon() : base() { }

    public BurstWeapon(BurstWeaponConfig config, bool isDefault = false)
        : base(config, isDefault)
    {
        projectilePrefab = config.ProjectilePrefab;
        burstRadius = config.BurstRadius;
        numOfShots = config.NumOfShots;

        lastShotFired = 0;
        isKeyReleased = true;
    }

    public override void Activate()
    {
        if (CheckIsShotReady() && isKeyReleased)
        {
            isKeyReleased = false;
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
        isKeyReleased = true;
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
        projectile.SetDamage(damage.GetCalcValue());
        Vector2 velocity = turret.transform.right * speed.GetCalcValue();
        projectile.SetVelocity(velocity);

        return projectile;
    }

    private bool CheckIsShotReady()
    {
        bool isShotReady = false;

        if (Time.time - lastShotFired >= shotCooldown.GetCalcValue())
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
