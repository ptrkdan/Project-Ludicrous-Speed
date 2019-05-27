using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnRateMin = 0.1f;
    [SerializeField] float spawnRateMax = 1f;
    [SerializeField] int maxEnemies = 20;
    [SerializeField] EnemyController EnemyPrefab;
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool looping = false;

    private float spawnPointX;
    private float spawnPointYMin;
    private float spawnPointYMax;
    private float nextTimeToSpawn = 0f;
    private int activeEnemiesCount = 0;
    private int startingWave = 0;
    private Quaternion rotateLeft = Quaternion.Euler(new Vector3(0, 0, -90));
    private Quaternion rotateRight = Quaternion.Euler(new Vector3(0, 0, 90));

    private IEnumerator Start() {
        SetSpawnRange();

        do {
            yield return (StartCoroutine(SpawnAllWaves()));
        }
        while (looping);

    }

    private void SetSpawnRange() {
        Camera gameCamera = Camera.main;
        spawnPointX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        spawnPointYMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        spawnPointYMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }
   
    private void Update() {
        SpawnStraightLineEnemy();
    }

    private void SpawnStraightLineEnemy() {
        if (Time.time >= nextTimeToSpawn) {
            Vector3 spawnPosition = new Vector3(-spawnPointX, Random.Range(spawnPointYMin, spawnPointYMax));
            Instantiate(EnemyPrefab, spawnPosition, Quaternion.identity);
            nextTimeToSpawn = Time.time + 1f / Random.Range(spawnRateMin, spawnRateMax);
        }
    }

    private IEnumerator SpawnAllWaves() {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++) {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig) {
        for (int i = 0; i < waveConfig.NumberOfEnemies; i++) {
            WaypointEnemyController enemy = Instantiate(
                waveConfig.GetEnemyPrefab(), 
                waveConfig.GetWayPoints()[0].transform.position, 
                rotateLeft);
            enemy.WaveConfig = waveConfig;

            yield return new WaitForSeconds(waveConfig.TimeBetweenSpawns);
        }
    }

    public void SetDifficulty(int difficulty) {

    }

    public void IncreaseEnemyCount() {
        activeEnemiesCount += 1;
    }

    public void DecreaseEnemyCount() {
        activeEnemiesCount -= 1;
    }

    
}
