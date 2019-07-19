using UnityEngine;

public class FireWeaponBehaviour : WeaponBehaviour
{
    float shotCounter;

    public override void Do()
    {
        CountdownAndShoot();
    }

    public override void SetWeapon(EnemyWeapon weapon)
    {
        base.SetWeapon(weapon);
        ResetShotCooldown();
    }

    private void CountdownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            FireWeapon();
            ResetShotCooldown();
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
