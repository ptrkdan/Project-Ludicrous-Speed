using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject {
    [SerializeField] WaypointEnemyController enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 5f;

    
    public WaypointEnemyController GetEnemyPrefab() { return enemyPrefab;  }
    public float TimeBetweenSpawns { get => timeBetweenSpawns; set => timeBetweenSpawns = value; }
    public float SpawnRandomFactor { get => spawnRandomFactor; set => spawnRandomFactor = value; }
    public int NumberOfEnemies { get => numberOfEnemies; set => numberOfEnemies = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    public List<Transform> GetWayPoints() {
        return new List<Transform>(pathPrefab.GetComponentsInChildren<Transform>());  
    }
}
