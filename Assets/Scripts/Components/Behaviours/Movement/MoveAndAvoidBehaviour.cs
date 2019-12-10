using UnityEngine;

public class MoveAndAvoidBehaviour : MovementBehaviour
{
    [SerializeField] LayerMask avoidLayers;
    [SerializeField] float lookAheadDistance = 10f;

    float yDirection = 0;
    bool isDangerAhead = false;

    public override BehaviourState Do(BehaviourState currentState)
    {
        CheckAhead();
        Move();

        return currentState | BehaviourState.Moved;
    }

    protected override void Move()
    {
        float engineFactor = stats.GetStat(StatType.Engine).GetCalcValue();
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
        if (lookAhead.collider != null)
        {
            isDangerAhead = true;
        }
        else
        {
            isDangerAhead = false;
        }
    }
}
