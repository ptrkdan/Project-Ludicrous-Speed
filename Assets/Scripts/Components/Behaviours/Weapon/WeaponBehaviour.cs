using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehaviour : Behaviour
{
    protected bool isFired;
    protected float shotCounter;

    protected EnemyWeapon weapon;
    public EnemyWeapon GetWeapon() => weapon;
    public virtual void SetWeapon(EnemyWeapon weapon)
    {
        this.weapon = weapon;
        ResetShotCooldown();
    }

    protected virtual void FireWeapon()
    {
        weapon.Activate();
    }

    protected virtual void ResetShotCooldown()
    {
        float cooldown = weapon.GetShotCooldown().GetCalcValue();
        float variation = weapon.GetCooldownVariation();
        shotCounter = Random.Range(cooldown - variation, cooldown + variation);
    }

    protected BehaviourState SetNewBehaviourState(BehaviourState currentState)
    {
        return isFired ? currentState | BehaviourState.Fired : currentState & (~BehaviourState.Fired);
    }

    protected abstract void CountdownAndShoot();
}
