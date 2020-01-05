using UnityEngine;

public class MoveAndAvoidBehaviour : MovementBehaviour
{
    [SerializeField] private LayerMask avoidLayers;
    [SerializeField] private float lookAheadDistance = 10f;

    private float yDirection = 0;
    private bool isDangerAhead = false;

    public override BehaviourState Do()
    {
        CheckAhead();
        Move();
        SetBehaviourState();

        return CurrentState;
    }

    protected override void Move()
    {
        float engineFactor = stats.GetStat(StatType.Engine).Value;
        Vector3 xMovement = Vector3.right
            * transform.parent.localScale.x
            * engineFactor
            * Time.deltaTime;

        Vector3 yMovement = Vector3.zero;
        if (!isDangerAhead)
        {
            yDirection = Random.Range(0, 1f) > 0.5f ? -1 : 1;
        }
        else
        {
            yMovement = Vector3.up
                * yDirection
                * engineFactor / 2
                * Time.deltaTime;
        }

        Vector3 movement = xMovement + yMovement;
        rigidBody.MovePosition(transform.position + movement);
    }

    private void CheckAhead()
    {
        RaycastHit2D lookAhead =
            Physics2D.CapsuleCast(
                transform.position,
                new Vector2(1, 1),
                CapsuleDirection2D.Vertical,
                0, // Angle 
                Vector2.right,
                lookAheadDistance,
                avoidLayers);
        isDangerAhead = lookAhead.collider != null;
    }
}
