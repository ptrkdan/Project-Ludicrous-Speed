using UnityEngine;

public class SecurityPatrolShip : SecurityUnitController
{
    protected override void Move()
    {
        rigidBody.MovePosition(transform.position + Vector3.right * stats.GetStat(StatType.Engine).GetCalcValue() * Time.fixedDeltaTime);
    }
}