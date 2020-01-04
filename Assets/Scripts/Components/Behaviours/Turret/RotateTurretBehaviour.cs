using UnityEngine;

public class RotateTurretBehaviour : TurretBehaviour
{
    [SerializeField, Tooltip("Rotation speed, in degrees")]
    private float turretRotationSpeed = 5f;

    [SerializeField] private float maxAngle;
    [SerializeField] private float minAngle;

    private float turretAngle = 0;
    private bool isRotatingCW;

    #region Methods: Unity 

    private void Start()
    {
        RotateTurret();     // Sets initial rotation
    }

    #endregion Methods: Unity 

    public override BehaviourState Do()
    {
        // Only rotate after firing weapon
        if (CurrentState.HasFlag(BehaviourState.Fired))
        {
            RotateTurret();
        }

        return CurrentState;
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
}
