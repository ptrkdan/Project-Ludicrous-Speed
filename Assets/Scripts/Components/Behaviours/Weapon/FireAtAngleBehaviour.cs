using UnityEngine;

public class FireAtAngleBehaviour : WeaponBehaviour
{
    [SerializeField] float shootUpAngle = 90f;
    [SerializeField] float shootDownAngle = -90f;

    public override BehaviourState Do(BehaviourState currentState)
    {
        CountdownAndShoot();

        return SetNewBehaviourState(currentState);
    }

    public override void SetWeapon(EnemyWeapon weapon)
    {
        base.SetWeapon(weapon);
        SetTurretAngle();
    }

    protected override void CountdownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            FireWeapon();
            ResetShotCooldown();
            isFired = true;
        }
    }

    private void SetTurretAngle()
    {
        Transform turret = weapon.GetTurretPosition();

        // Find player position
        PlayerController player = FindObjectOfType<PlayerController>();
        bool isPlayerAbove = player.transform.position.y > transform.position.y;

        float angle;
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
