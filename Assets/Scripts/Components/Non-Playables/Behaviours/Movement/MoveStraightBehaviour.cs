using UnityEngine;

public class MoveStraightBehaviour : MovementBehaviour
{
    public override void Do()
    {
        Move();
    }

    protected override void Move()
    {
        float engineFactor = stats.GetStat(StatType.Engine).GetCalcValue();
        Vector3 movement = Vector3.right 
            * transform.parent.localScale.x 
            * engineFactor 
            * Time.fixedDeltaTime;
        rigidBody.MovePosition(transform.position + movement);
    }

}
