using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Spawn parameters")]
    [SerializeField] Vector2 spawnDelayRange = new Vector2(1, 10);
    [SerializeField] List<AsteroidController> asteroidList;

    [Header("Asteroid stats")]
    [SerializeField] float baseHealth = 100;
    [SerializeField] Vector2 scaleRange = new Vector2(1, 10);
    [SerializeField] float baseSpeed = 5f;
    [SerializeField] Vector2 speedRange = new Vector2(0.1f, 5);
    [SerializeField] float randomSpeedMin = 0.1f;
    [SerializeField] float randomSpeedMax = 5f;

    bool spawning = true;
    Transform[] spawnPoints;
    Transform currentSpawnPoint;

    private IEnumerator Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();

        while (spawning)
        {
            yield return new WaitForSeconds(
                Random.Range(spawnDelayRange.x, spawnDelayRange.y)
            );
            SpawnAsteroid();
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

    private void SpawnAsteroid()
    {
        currentSpawnPoint = spawnPoints[Random.Range(1, spawnPoints.Length)]; // spawnPoints[0] = this
        float randomSpin = Random.Range(0, 180f);
        float randomScale = Random.Range(scaleRange.x, scaleRange.y);
        int randomMeteor = Random.Range(0, asteroidList.Count);

        float randomMoveSpeed = Random.Range(randomSpeedMin, randomSpeedMax);
        StatModifier speedMod = new StatModifier(gameObject, StatType.Engine, StatModType.Flat, baseSpeed + randomMoveSpeed);

        //Debug.Log($"Spawning asteroid at vector {currentSpawnPoint.position.y}");

        AsteroidController newAsteroid =
            Instantiate(asteroidList[randomMeteor],
            currentSpawnPoint.position,
            currentSpawnPoint.rotation) as AsteroidController;
        newAsteroid.transform.parent = currentSpawnPoint.transform;
        newAsteroid.transform.localScale = new Vector3(randomScale, randomScale, 0);
        newAsteroid.GetComponent<Rigidbody2D>().MoveRotation(randomSpin);
        newAsteroid.GetComponent<AsteroidStats>().GetStat(StatType.Engine).AddModifier(speedMod);
        newAsteroid.GetComponent<AsteroidStats>().SetCurrentHealth(baseHealth * randomScale / 3);
    }

    public void SetDifficulty(int difficulty)
    {
        spawnDelayRange.x = 5 / (difficulty + 2) - 0.3f;  // TODO Set const
        spawnDelayRange.y = 10 / (difficulty + 2) - 0.5f;  // TODO Set const
        scaleRange.x = difficulty;              // TODO Set const
        scaleRange.y = difficulty + 8;          // TODO Set const
    }
}
