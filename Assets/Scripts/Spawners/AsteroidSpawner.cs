using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] float spawnRateMin = 1f;
    [SerializeField] float spawnRateMax = 3f;
    [SerializeField] int baseHealth = 100;
    [SerializeField] int randomScaleMin = 2;
    [SerializeField] int randomScaleMax = 8;
    [SerializeField] float baseSpeed = 5f;
    [SerializeField] float randomSpeedMin = 0.1f;
    [SerializeField] float randomSpeedMax = 5f;
    [SerializeField] int maxMeteor = 10;                    // Needed?
    [SerializeField] List<AsteroidController> asteroidList;

    private float spawnPointX;
    private float spawnPointYMin;
    private float spawnPointYMax;
    private float nextTimeToSpawn = 0f;
    private int activeMeteorCount;

    private void Start() {
        SetSpawningArea();
    }

    private void Update() {
        if (Time.time >= nextTimeToSpawn) {
            SpawnMeteor();
            nextTimeToSpawn = Time.time + 1f / Random.Range(spawnRateMin, spawnRateMax);
        }
    }

    private void SetSpawningArea() {
        Camera gameCamera = Camera.main;
        spawnPointX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        spawnPointYMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        spawnPointYMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    private void SpawnMeteor() {
        Vector3 spawnPosition = new Vector3(spawnPointX, Random.Range(spawnPointYMin, spawnPointYMax));
        float randomSpin = Random.Range(0, 180f);
        int randomScale = Random.Range(randomScaleMin, randomScaleMax);
        int randomMeteor = Random.Range(0, asteroidList.Count);

        float randomMoveSpeed = Random.Range(randomSpeedMin, randomSpeedMax);
        StatModifier speedMod = new StatModifier(gameObject, StatType.Engine,StatModType.Flat, baseSpeed + randomMoveSpeed);

        AsteroidController newAsteroid = 
            Instantiate(asteroidList[randomMeteor], spawnPosition, Quaternion.identity) as AsteroidController;
        newAsteroid.transform.parent = transform;
        newAsteroid.transform.localScale = new Vector3(randomScale, randomScale, 0);
        newAsteroid.GetComponent<Rigidbody2D>().MoveRotation(randomSpin);
        newAsteroid.GetComponent<AsteroidStats>().GetStat(StatType.Engine).AddModifier(speedMod);
        newAsteroid.GetComponent<AsteroidStats>().SetCurrentHealth(baseHealth * randomScale);
    }

    public void SetDifficulty(int difficulty) {
        spawnRateMin = difficulty * 0.5f;
        spawnRateMax = difficulty + 2.5f;
        randomScaleMax = difficulty;
        randomScaleMax = difficulty + 5;
    }

    public void IncreaseAsteroidCount() {
        activeMeteorCount += 1;
    }

    public void DecreaseAsteroidCount() {
        activeMeteorCount -= 1;
    }

}
