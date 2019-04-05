﻿using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] float spawnRateMin = 1f;
    [SerializeField] float spawnRateMax = 3f;
    [SerializeField] int randomScaleMin = 2;
    [SerializeField] int randomScaleMax = 8;
    [SerializeField] float randomSpeedMin = 0.1f;
    [SerializeField] float randomSpeedMax = 5f;
    [SerializeField] int maxMeteor = 10;                    // Needed?
    [SerializeField] List<AsteroidController> meteorList;

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
        float randomMoveSpeed = Random.Range(randomSpeedMin, randomSpeedMax);
        int randomScale = Random.Range(randomScaleMin, randomScaleMax);
        int randomMeteor = Random.Range(0, meteorList.Count);

        AsteroidController newMeteor = Instantiate(meteorList[randomMeteor], spawnPosition, Quaternion.identity);
        newMeteor.GetComponent<Rigidbody2D>().MoveRotation(randomSpin);
        newMeteor.transform.localScale = new Vector3(randomScale, randomScale, 0);
        newMeteor.MoveSpeed = randomMoveSpeed;
        newMeteor.Health *= randomScale;
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
