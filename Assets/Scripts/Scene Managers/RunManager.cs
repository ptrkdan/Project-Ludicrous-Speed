using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunManager : MonoBehaviour
{
    [SerializeField] GameSession session;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] PlayerController player;
    [SerializeField] MeteorSpawner meteorSpawner;
    [SerializeField] EnemySpawner enemySpanwer;

    [SerializeField] TextMeshProUGUI distanceRemainingText;

    int distanceRemaining;

    // Start is called before the first frame update
    void Start()
    {
        session = FindObjectOfType<GameSession>();

        ConfigureRun();
    }

    void FixedUpdate() {
        UpdateDistanceRemaining();
    }

    private void ConfigureRun() {
        int difficulty = session.ActiveContract.GetContractDifficultyLevel();
        distanceRemaining = session.ActiveContract.GetRunDistance();
        distanceRemainingText.text = distanceRemaining.ToString();
        ConfigureMeteorSpawner(difficulty);
        ConfigureEnemySpawner(difficulty);
    }

    private void ConfigureMeteorSpawner(int difficulty) {
        meteorSpawner.SetDifficulty(difficulty);
    }

    private void ConfigureEnemySpawner(int difficulty) {
        enemySpanwer.SetDifficulty(difficulty);
    }
    
    private void UpdateDistanceRemaining() {
        if (distanceRemaining > 0) {
            distanceRemaining -= 1;
            distanceRemainingText.text = distanceRemaining.ToString();
        }
        else {
            session.IsRunSuccessful = true;
            sceneLoader.WaitAndLoadRunResultsScene(2f);
        }
    }
}
