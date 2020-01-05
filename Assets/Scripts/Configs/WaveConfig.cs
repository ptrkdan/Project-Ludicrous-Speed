using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [Header("Prefabs")]
    [SerializeField] private WaypointEnemyController enemyPrefab;
    [SerializeField] private GameObject pathPrefab;

    [Header("Spawn Parameters")]
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    [SerializeField] private float spawnRandomFactor = 0.3f;
    [SerializeField] private int numberOfEnemies = 5;
    [SerializeField] private float moveSpeed = 5f;


    public WaypointEnemyController GetEnemyPrefab() { return enemyPrefab; }
    public float TimeBetweenSpawns { get => timeBetweenSpawns; set => timeBetweenSpawns = value; }
    public float SpawnRandomFactor { get => spawnRandomFactor; set => spawnRandomFactor = value; }
    public int NumberOfEnemies { get => numberOfEnemies; set => numberOfEnemies = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    public List<Transform> GetWayPoints()
    {
        return new List<Transform>(pathPrefab.GetComponentsInChildren<Transform>());
    }
}
