using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    [SerializeField] int spawnDistanceToPlayer = 2;
    [SerializeField] Vector2 spawnDelayRange = new Vector2(1, 10);
    [SerializeField] List<CreatureController> creatureUnitPrefabs;

    bool spawning = true;
    PlayerController player;
    Transform[] spawnPoints;
    List<int> bottomSpawnPoints;
    List<int> midSpawnPoints;
    List<int> topSpawnPoints;
    List<int> topBottomSpawnPoints;

    private IEnumerator Start()
    {
        player = FindObjectOfType<PlayerController>();
        SetSpawnPoints();

        while (spawning)
        {
            yield return new WaitForSeconds(Random.Range(spawnDelayRange.x, spawnDelayRange.y));
            if (creatureUnitPrefabs.Count > 0)
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
        // TODO: Implement difficulty factor
    }

    public void SetCreatureUnitPrefabs(List<CreatureController> creatureUnitPrefabs)
    {
        this.creatureUnitPrefabs = creatureUnitPrefabs;
    }

    private void SetSpawnPoints()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        int numSpawnPoints = spawnPoints.Length - 1; // spawnPoints[0] == this

        // Divide spawn points into 3 segments: top, mid, bottom
        int bottomMidDivide = Mathf.FloorToInt(numSpawnPoints / 4);
        int topMidDivide = numSpawnPoints - Mathf.CeilToInt(numSpawnPoints / 4);

        bottomSpawnPoints = new List<int>();
        for (int i = 0; i <= bottomMidDivide; i++)
        {
            bottomSpawnPoints.Add(i);
        }
        midSpawnPoints = new List<int>();
        for (int i = bottomMidDivide; i <= topMidDivide; i++)
        {
            midSpawnPoints.Add(i);
        }
        topSpawnPoints = new List<int>();
        for (int i = topMidDivide; i <= spawnPoints.Length - 1; i++)
        {
            topSpawnPoints.Add(i);
        }
        topBottomSpawnPoints = new List<int>();
        topBottomSpawnPoints.AddRange(topSpawnPoints);
        topBottomSpawnPoints.AddRange(bottomSpawnPoints);
    }

    private void SpawnUnit()
    {
        EnemyController unitPrefab = creatureUnitPrefabs[Random.Range(0, creatureUnitPrefabs.Count)];
        Transform selectedSpawnPoint = SelectSpawnPoint(unitPrefab.GetSpawnPreference());

        EnemyController newUnit = Instantiate(unitPrefab, selectedSpawnPoint.position, selectedSpawnPoint.rotation);
        newUnit.transform.parent = selectedSpawnPoint.transform;
    }

    private Transform SelectSpawnPoint(SpawnPreference spawnPreference)
    {
        int randomIndex = -1;
        switch (spawnPreference)
        {
            case (SpawnPreference.NearPlayer):
                // Get Player location
                int playerPosition = Mathf.RoundToInt(player.GetComponent<Transform>().position.y);
                // Select spawn point +/- spawnDistanceToPlayer
                int minSpawnPosition =
                    Mathf.Clamp(playerPosition - spawnDistanceToPlayer, 0, spawnPoints.Length - 1);
                int maxSpawnPosition =
                    Mathf.Clamp(playerPosition + spawnDistanceToPlayer, 0, spawnPoints.Length - 1);

                randomIndex = Random.Range(minSpawnPosition, maxSpawnPosition);
                break;
            case (SpawnPreference.Top):
                randomIndex = topSpawnPoints[Random.Range(0, topSpawnPoints.Count)];
                break;
            case (SpawnPreference.Bottom):
                randomIndex = bottomSpawnPoints[Random.Range(0, bottomSpawnPoints.Count)];
                break;
            case (SpawnPreference.TopBottom):
                randomIndex = topBottomSpawnPoints[Random.Range(0, topBottomSpawnPoints.Count)];
                break;
            case (SpawnPreference.Middle):
                randomIndex = midSpawnPoints[Random.Range(0, midSpawnPoints.Count)];
                break;
            case (SpawnPreference.Anywhere):
                randomIndex = Random.Range(1, spawnPoints.Length); // spawnPoint[0] == this
                break;
            default:
                break;
        }


        return spawnPoints[randomIndex];
    }
}
