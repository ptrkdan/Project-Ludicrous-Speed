using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuritySpawner : MonoBehaviour
{
    [SerializeField] int spawnDistanceToPlayer = 2;
    [SerializeField] Vector2 spawnDelayRange = new Vector2(1, 10);
    [SerializeField] List<EnemyController> securityUnitPrefabs;
    [SerializeField] List<WaveConfig> waveConfigs;

    bool spawning = true;
    PlayerController player;
    Transform[] spawnPoints;
    Transform currentSpawnPoint;
    //int startingWave = 0;

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

    public void SetDifficulty(int difficulty)
    {
        // TODO Implement difficulty factor
    }

    public void SetSecurityUnitPrefabs(List<EnemyController> securityUnitPrefabs)
    {
        this.securityUnitPrefabs = securityUnitPrefabs;
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

        // Create new unit
        EnemyController unitPrefab = securityUnitPrefabs[Random.Range(0, securityUnitPrefabs.Count)];
        EnemyController newUnit = Instantiate(unitPrefab, currentSpawnPoint.position, currentSpawnPoint.rotation) as EnemyController;
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
}
