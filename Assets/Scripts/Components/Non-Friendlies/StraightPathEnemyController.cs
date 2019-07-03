using UnityEngine;

public class StraightPathEnemyController : EnemyController
{
    protected override void Move() {
        rigidBody.MovePosition(transform.position + Vector3.right * stats.GetStat(StatType.Engine).GetCalcValue() * Time.fixedDeltaTime);
    }
}
