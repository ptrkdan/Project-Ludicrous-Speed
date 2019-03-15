using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointEnemyController : EnemyController {
    int waypointIndex = 1;

    public WaveConfig WaveConfig { get; set; }

    protected override void Move() {
        if (waypointIndex <= WaveConfig.GetWayPoints().Count - 1) {
            var targetPosition = WaveConfig.GetWayPoints()[waypointIndex].transform.position;
            var currentMovement = WaveConfig.MoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, currentMovement);

            if (transform.position == targetPosition) {
                waypointIndex++;
            }
        }
    }


}
