using UnityEngine;

public class MoveStraightBehaviour : MovementBehaviour
{
    public override BehaviourState Do()
    {
        Move();
        SetBehaviourState();

        return CurrentState;
    }

    protected override void Move()
    {
        float engineFactor = stats.GetStat(StatType.Engine).Value;
        Vector3 movement = Vector3.right
            * transform.parent.localScale.x
            * engineFactor
            * Time.deltaTime;
        rigidBody.MovePosition(transform.position + movement);
    }

}
