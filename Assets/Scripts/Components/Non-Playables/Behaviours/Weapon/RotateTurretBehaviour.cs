using UnityEngine;

public class RotateTurretBehaviour : WeaponBehaviour
{
    [SerializeField, Tooltip("Rotation speed, in degrees")] float turretRotationSpeed = 5f;

    Transform turret;
    float maxAngle;
    float minAngle;
    float turretAngle = 0;
    bool isRotatingCW;

    public override BehaviourState Do(BehaviourState currentState)
    {
        // Only rotate after firing weapon
        if (currentState.HasFlag(BehaviourState.Fired))
        {
            RotateTurret();
        }

        return currentState;
    }

    public override void SetWeapon(EnemyWeapon weapon)
    {
        base.SetWeapon(weapon);
        SetTurretAngle();
        RotateTurret();
    }

    private void RotateTurret()
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
}
