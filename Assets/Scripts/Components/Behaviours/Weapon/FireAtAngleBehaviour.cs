using UnityEngine;

public class FireAtAngleBehaviour : WeaponBehaviour
{
    private const float DEFAULT_ANGLE = 90f;

    [SerializeField] private float shootUpAngle = DEFAULT_ANGLE;
    [SerializeField] private float shootDownAngle = -DEFAULT_ANGLE;

    public override BehaviourState Do()
    {
        CountdownAndShoot();
        SetBehaviourState();

        return CurrentState;
    }

    public override void SetWeapon(EnemyWeapon weapon)
    {
        base.SetWeapon(weapon);
        SetTurretAngle();
    }

    #region Private Methods

    private void SetTurretAngle()
    {
        Transform turret = Weapon.GetTurretPosition();

        // Find player position
        PlayerController player = FindObjectOfType<PlayerController>();
        bool isPlayerAbove = player.transform.position.y > transform.position.y;

        float angle = isPlayerAbove ? shootUpAngle : shootDownAngle;
        turret.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    #endregion Private Methods
}
