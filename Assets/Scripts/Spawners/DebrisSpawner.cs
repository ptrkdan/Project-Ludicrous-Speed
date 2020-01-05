using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour
{
    [Header("Spawn parameters")]
    [SerializeField] private Vector2 spawnDelayRange = new Vector2(1, 10);
    [SerializeField] private List<DebrisController> debrisPrefabs;

    [Header("Debris stats")]
    [SerializeField] private float baseHealth = 100;
    [SerializeField] private Vector2 scaleRange = new Vector2(1, 10);
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private Vector2 speedRange = new Vector2(0.1f, 5);
    [SerializeField] private float randomSpeedMin = 0.1f;
    [SerializeField] private float randomSpeedMax = 5f;
    private bool spawning = true;
    private Transform[] spawnPoints;
    private Transform currentSpawnPoint;

    private IEnumerator Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();

        while (spawning)
        {
            yield return new WaitForSeconds(Random.Range(spawnDelayRange.x, spawnDelayRange.y));
            if (debrisPrefabs.Count > 0)
                SpawnDebris();
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
        spawnDelayRange.x = 5 / (difficulty + 2) - 0.3f;  // TODO Set const
        spawnDelayRange.y = 10 / (difficulty + 2) - 0.5f;  // TODO Set const
        scaleRange.x = difficulty;              // TODO Set const
        scaleRange.y = difficulty + 8;          // TODO Set const
    }

    public void SetDebrisPrefabs(List<DebrisController> debrisPrefabs)
    {
        this.debrisPrefabs = debrisPrefabs;
    }

    private void SpawnDebris()
    {
        currentSpawnPoint = spawnPoints[Random.Range(1, spawnPoints.Length)]; // spawnPoints[0] = this
        float randomSpin = Random.Range(0, 180f);
        float randomScale = Random.Range(scaleRange.x, scaleRange.y);
        int randomDebris = Random.Range(0, debrisPrefabs.Count);

        float randomMoveSpeed = Random.Range(randomSpeedMin, randomSpeedMax);
        StatModifier speedMod = new StatModifier(gameObject, StatType.Engine, StatModType.Flat, baseSpeed + randomMoveSpeed);

        DebrisController newDebris =
            Instantiate(debrisPrefabs[randomDebris],
            currentSpawnPoint.position,
            currentSpawnPoint.rotation) as DebrisController;
        newDebris.transform.parent = currentSpawnPoint.transform;
        newDebris.transform.localScale = new Vector3(randomScale, randomScale, 0);
        newDebris.GetComponent<Rigidbody2D>().MoveRotation(randomSpin);
        newDebris.GetComponent<AsteroidStats>().GetStat(StatType.Engine).AddModifier(speedMod);
        newDebris.GetComponent<AsteroidStats>().CurrentHealth = baseHealth * randomScale / 3;
    }
}
