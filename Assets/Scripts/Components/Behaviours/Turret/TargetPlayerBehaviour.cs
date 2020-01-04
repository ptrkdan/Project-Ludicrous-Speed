public class TargetPlayerBehaviour : TurretBehaviour
{
    public override BehaviourState Do()
    {
        RotateTurret();

        return CurrentState;
    }

    protected override void SetTurretAngle()
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        // Find angle towards player

        // Set turret angle
    }

    private void RotateTurret()
    {

    }
}
