using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienPassengerShip : CreatureController
{
    protected override void Move()
    {
        rigidBody.MovePosition(transform.position + Vector3.right * stats.GetStat(StatType.Engine).GetCalcValue() * Time.fixedDeltaTime);
    }

    protected override void CountDownAndShoot()
    {
        return;         // Has no weapons :)
    }
}
