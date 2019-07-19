using UnityEngine;

public class RotateTurretAndFireWeaponBehaviour : WeaponBehaviour
{
    [SerializeField, Tooltip("Rotation speed, in degrees")] float turretRotationSpeed = 5f;

    float shotCounter;

    Transform turret;
    float maxAngle;
    float minAngle;
    float turretAngle = 0;
    bool isRotatingCW;
    
    public override void Do()
    {
        RotateAndFire();
    }

    public override void SetWeapon(EnemyWeapon weapon)
    {
        base.SetWeapon(weapon);
        SetTurretAngle();
        ResetShotCooldown();
    }

    private void RotateAndFire()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            RotateWeaponTurret();
            FireWeapon();
            ResetShotCooldown();
        }
    }

    private void FireWeapon()
    {
        weapon.Activate();
    }

    private void RotateWeaponTurret()
    {
        turret.rotation = Quaternion.AngleAxis(turretAngle, Vector3.forward);
        if (isRotatingCW)
        {
            turretAngle += turretRotationSpeed;
            if (turretAngle >= maxAngle) isRotatingCW = false;
        }
        else
        {
            turretAngle -= turretRotationSpeed;
            if (turretAngle <= minAngle) isRotatingCW = true;
        }
    }

    private void SetTurretAngle()
    {
        turret = weapon.GetTurretPosition();

        // Find player position
        PlayerController player = FindObjectOfType<PlayerController>();
        bool isPlayerAbove = player.transform.position.y > transform.position.y;

        if (isPlayerAbove)
        {
            minAngle = 60;
            maxAngle = 120;
            turretAngle = minAngle;
            isRotatingCW = false;
        }
        else
        {
            minAngle = 240;
            maxAngle = 300;
            turretAngle = maxAngle;
            isRotatingCW = true;
        }
    }

    private void ResetShotCooldown()
    {
        float cooldown = weapon.GetShotCooldown().GetCalcValue();
        float variation = weapon.GetCooldownVariation();
        shotCounter = Random.Range(cooldown - variation, cooldown + variation);
    }
}
