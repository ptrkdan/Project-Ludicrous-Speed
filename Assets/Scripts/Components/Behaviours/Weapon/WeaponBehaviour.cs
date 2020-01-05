using UnityEngine;

public abstract class WeaponBehaviour : Behaviour
{
    protected bool isFired;
    protected float shotCounter;

    public EnemyWeapon Weapon { get; private set; }

    #region Methods: Weapon

    public virtual void SetWeapon(EnemyWeapon weapon)
    {
        Weapon = weapon;
        ResetShotCooldown();
    }

    protected virtual void FireWeapon()
    {
        Weapon.Activate();
    }

    protected virtual void ResetShotCooldown()
    {
        float cooldown = Weapon.GetShotCooldown().Value;
        float variation = Weapon.GetCooldownVariation();
        shotCounter = Random.Range(cooldown - variation, cooldown + variation);
    }

    protected virtual void CountdownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        isFired = false;
        if (shotCounter <= 0)
        {
            FireWeapon();
            ResetShotCooldown();
            isFired = true;
        }
    }

    #endregion Methods: Weapon 

    #region Methods: Behaviour State

    protected override void SetBehaviourState()
    {
        CurrentState = isFired ? CurrentState | BehaviourState.Fired : CurrentState & (~BehaviourState.Fired);
    }

    #endregion Methods: Behaviour State
}
