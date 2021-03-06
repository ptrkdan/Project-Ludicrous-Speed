﻿using System.Collections;
using UnityEngine;

public class SemiAutoWeapon : Weapon
{
    Stat burstCooldown;
    int numShots;
    float lastShotFired = 0;
    bool isTriggerReleased;

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
        projectile.SetDamage(damage.GetCalcValue());
        Vector2 velocity = turret.transform.right * speed.GetCalcValue();
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

            yield return new WaitForSeconds(shotCooldown.GetCalcValue());
            shotsFired++;
        }
        
    }

    private bool IsShotReady()
    {
        bool isShotReady = false;
        if (Time.time - lastShotFired >= burstCooldown.GetCalcValue())
        {
            isShotReady = true;
        }

        return isShotReady;
    }
}
