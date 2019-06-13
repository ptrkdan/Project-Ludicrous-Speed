using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuritySpawner : MonoBehaviour
{
    Quaternion rotateLeft = Quaternion.Euler(new Vector3(0, 0, -90));

    [SerializeField] int spawnDistanceToPlayer = 2;
    [SerializeField] Vector2 spawnDelay = new Vector2(1, 10);
    [SerializeField] EnemyController SecurityUnitPrefab;
    [SerializeField] List<WaveConfig> waveConfigs;

    bool spawning = true;
    PlayerController player;
    Transform[] spawnPoints;
    Transform currentSpawnPoint;
    //int startingWave = 0;
    //private Quaternion rotateRight = Quaternion.Euler(new Vector3(0, 0, 90));

    private IEnumerator Start() {
        player = FindObjectOfType<PlayerController>();
        spawnPoints = GetComponentsInChildren<Transform>();

        do {
            yield return new WaitForSeconds(Random.Range(spawnDelay.x, spawnDelay.y));
            SpawnUnit();
        }
        while (spawning);

    }

    private void SpawnUnit() {
        // Get Player location
        int playerPosition = Mathf.RoundToInt(player.GetComponent<Transform>().position.y);
        // Select spawn point +/- spawnDistanceToPlayer
        currentSpawnPoint = spawnPoints[
            Random.Range(playerPosition - spawnDistanceToPlayer, playerPosition + spawnDistanceToPlayer)];

        Debug.Log($"Spawning security unit at vector {currentSpawnPoint.position.y}");

        // Create new unit
        EnemyController newUnit = Instantiate(SecurityUnitPrefab, currentSpawnPoint.position, currentSpawnPoint.rotation) as EnemyController;
        newUnit.transform.parent = transform;
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

    public void SetDifficulty(int difficulty) {

    }    
}
