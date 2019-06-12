﻿using System.Collections;
using System.Collections.Generic;
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

    public override void Activate(Vector3 weaponPosition, Quaternion weaponRotation)
    {
        base.Activate(weaponPosition, weaponRotation);
        if (!chargingProjectile)
        {
            chargingProjectile = Instantiate(
                projectilePrefab,
                weaponPosition,
                weaponRotation) as ChargedProjectile;
            chargingProjectile.SetDamage((int)damage.GetCalcValue());
            chargingProjectile.SetSpeed(speed.GetCalcValue());
        }
        
    }

    public override void Deactivate()
    {
        if (chargingProjectile.IsCharged)
        {
            projectilePrefab.Fire();
            PlayFireSFX();
        }
    }
}
