using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuritySpawner : MonoBehaviour
{
    Quaternion rotateLeft = Quaternion.Euler(new Vector3(0, 0, -90));

    [SerializeField] int spawnDistanceToPlayer = 2;
    [SerializeField] Vector2 spawnDelayRange = new Vector2(1, 10);
    [SerializeField] EnemyController SecurityUnitPrefab;
    [SerializeField] List<WaveConfig> waveConfigs;

    bool spawning = true;
    PlayerController player;
    Transform[] spawnPoints;
    Transform currentSpawnPoint;
    //int startingWave = 0;
    //private Quaternion rotateRight = Quaternion.Euler(new Vector3(0, 0, 90));

    private IEnumerator Start()
    {
        player = FindObjectOfType<PlayerController>();
        spawnPoints = GetComponentsInChildren<Transform>();

        while (spawning)
        {
            yield return new WaitForSeconds(Random.Range(spawnDelayRange.x, spawnDelayRange.y));
            SpawnUnit();
        }

    }

    public void StartSpawning()
    {
        spawning = true;
    }

    public void StopSpawning()
    {
        spawning = false;
    }

    private void SpawnUnit()
    {
        // Get Player location
        int playerPosition = Mathf.RoundToInt(player.GetComponent<Transform>().position.y);
        // Select spawn point +/- spawnDistanceToPlayer
        int minSpawnPosition = 
            Mathf.Clamp(playerPosition - spawnDistanceToPlayer, 0, spawnPoints.Length-1);
        int maxSpawnPosition = 
            Mathf.Clamp(playerPosition + spawnDistanceToPlayer, 0, spawnPoints.Length-1);
        currentSpawnPoint = spawnPoints[Random.Range(minSpawnPosition, maxSpawnPosition)];

        //Debug.Log($"Spawning security unit at vector {currentSpawnPoint.position.y}");

        // Create new unit
        EnemyController newUnit = Instantiate(SecurityUnitPrefab, currentSpawnPoint.position, currentSpawnPoint.rotation) as EnemyController;
        newUnit.transform.parent = currentSpawnPoint.transform;
    }

    //private IEnumerator SpawnAllWaves() {
    //    for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++) {
    //        var currentWave = waveConfigs[waveIndex];
    //        yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    //    }
    //}

    //private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig) {
    //    for (int i = 0; i < waveConfig.NumberOfEnemies; i++) {
    //        WaypointEnemyController enemy = Instantiate(
    //            waveConfig.GetEnemyPrefab(), 
    //            waveConfig.GetWayPoints()[0].transform.position, 
    //            rotateLeft);
    //        enemy.WaveConfig = waveConfig;

    //        yield return new WaitForSeconds(waveConfig.TimeBetweenSpawns);
    //    }
    //}

    public void SetDifficulty(int difficulty)
    {

    }
}
