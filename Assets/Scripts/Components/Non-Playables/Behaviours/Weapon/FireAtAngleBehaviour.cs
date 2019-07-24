using UnityEngine;

public class FireAtAngleBehaviour : WeaponBehaviour
{
    [SerializeField] float shootUpAngle = 90f;
    [SerializeField] float shootDownAngle = -90f;

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
        SetTurretAngle();
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

    private void SetTurretAngle()
    {
        Transform turret = weapon.GetTurretPosition();

        // Find player position
        PlayerController player = FindObjectOfType<PlayerController>();
        bool isPlayerAbove = player.transform.position.y > transform.position.y;

        float angle = 0;
        if (isPlayerAbove)
        {
            angle = shootUpAngle;
        }
        else
        {
            angle = shootDownAngle;
        }

        turret.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
