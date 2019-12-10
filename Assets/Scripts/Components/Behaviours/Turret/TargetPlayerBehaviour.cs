using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayerBehaviour : TurretBehaviour
{
    public override BehaviourState Do(BehaviourState currentState)
    {
        RotateTurret();

        return currentState;
    }

    private void RotateTurret()
    {
        
    }

    protected override void SetTurretAngle()
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        // Find angle towards player

        // Set turret angle
    }
}
