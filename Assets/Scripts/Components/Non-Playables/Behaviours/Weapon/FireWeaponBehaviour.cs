﻿using UnityEngine;

public class FireWeaponBehaviour : WeaponBehaviour
{
    float shotCounter;
    BehaviourState newState = BehaviourState.None;

    public override BehaviourState Do(BehaviourState currentState)
    {
        CountdownAndShoot();

        return currentState | newState;
    }

    public override void SetWeapon(EnemyWeapon weapon)
    {
        base.SetWeapon(weapon);
        ResetShotCooldown();
    }

    private void CountdownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        newState = BehaviourState.None;
        if (shotCounter <= 0)
        {
            FireWeapon();
            ResetShotCooldown();
            newState = BehaviourState.Fired;
        }
    }

    private void FireWeapon()
    {
        weapon.Activate();
    }

    private void ResetShotCooldown()
    {
        float cooldown = weapon.GetShotCooldown().GetCalcValue();
        float variation = weapon.GetCooldownVariation();
        shotCounter = Random.Range(cooldown - variation, cooldown + variation);
    }
}
