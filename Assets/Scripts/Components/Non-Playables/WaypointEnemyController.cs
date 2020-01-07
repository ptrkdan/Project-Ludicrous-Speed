using UnityEngine;

public class WaypointEnemyController : EnemyController
{
    [SerializeField] private WaveConfig waveConfig;

    private int waypointIndex = 1;

    public WaveConfig WaveConfig { get => waveConfig; set => waveConfig = value; }

    protected void Move()
    {
        if (waypointIndex <= waveConfig.GetWayPoints().Count - 1)
        {
            Vector3 targetPosition = waveConfig.GetWayPoints()[waypointIndex].transform.position;
            float currentMovement = stats.GetStat(StatType.Engine).Value * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, currentMovement);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
    }


}
