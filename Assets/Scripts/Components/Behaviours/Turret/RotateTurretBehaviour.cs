using UnityEngine;

public class RotateTurretBehaviour : TurretBehaviour
{
    [SerializeField, Tooltip("Rotation speed, in degrees")] float turretRotationSpeed = 5f;

    float maxAngle;
    float minAngle;
    float turretAngle = 0;
    bool isRotatingCW;

    private void Start()
    {
        RotateTurret();     // Set initial rotation
    }

    public override BehaviourState Do(BehaviourState currentState)
    {
        // Only rotate after firing weapon
        if (currentState.HasFlag(BehaviourState.Fired))
        {
            RotateTurret();
        }

        return currentState;
    }

    private void RotateTurret()
    {
        foreach (Transform turret in turrets)
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
    }

    protected override void SetTurretAngle()
    {
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
