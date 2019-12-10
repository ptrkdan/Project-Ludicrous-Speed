using UnityEngine;

public class FireWeaponBehaviour : WeaponBehaviour
{

    public override BehaviourState Do(BehaviourState currentState)
    {
        CountdownAndShoot();

        return SetNewBehaviourState(currentState);
    }

    protected override void CountdownAndShoot()
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
}
