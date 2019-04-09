using UnityEngine;

public class WaypointEnemyController : EnemyController {
    [SerializeField] WaveConfig waveConfig;

    private int waypointIndex = 1;

    public WaveConfig WaveConfig { get => waveConfig; set => waveConfig = value; }

    protected override void Move() {
        if (waypointIndex <= waveConfig.GetWayPoints().Count - 1) {
            var targetPosition = waveConfig.GetWayPoints()[waypointIndex].transform.position;
            var currentMovement = stats.speed.GetCalcValue() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, currentMovement);

            if (transform.position == targetPosition) {
                waypointIndex++;
            }
        }
    }


}
