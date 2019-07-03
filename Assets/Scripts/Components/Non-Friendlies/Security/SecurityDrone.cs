using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityDrone : StraightPathEnemyController
{
    [SerializeField, Tooltip("Rotate speed, in degrees")] float turretRotateSpeed = 5f;

    float maxAngle;
    float minAngle;
    float turretAngle = 0;
    bool isRotatingCW;

    PlayerController player;

    protected override void Initialize()
    {
        base.Initialize();
        player = FindObjectOfType<PlayerController>();
        SetTurretAngle();
    }

    protected override void FireWeapon()
    {
        base.FireWeapon();
        MoveTurret();
    }

    private void MoveTurret()
    {
        turret.rotation = Quaternion.AngleAxis(turretAngle, Vector3.forward);
        if (isRotatingCW)
        {
            turretAngle += turretRotateSpeed;
            if (turretAngle >= maxAngle) isRotatingCW = false;
        }
        else
        {
            turretAngle -= turretRotateSpeed;
            if (turretAngle <= minAngle) isRotatingCW = true;
        }
    }

    private void SetTurretAngle()
    {
        // Find player position
        bool isPlayerAbove = player.transform.position.y > transform.position.y ? true : false;

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
