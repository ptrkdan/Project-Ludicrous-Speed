using UnityEngine;
using TMPro;

public class RunManager : MonoBehaviour
{
    [Header("Cached")]
    [SerializeField] GameSession session;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] PlayerController player;

    [Header("Spawners")]
    [SerializeField] AsteroidSpawner asteroidSpawner;
    [SerializeField] EnemySpawner enemySpanwer;

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI distanceRemainingText;

    private int distanceRemaining;

    private void Start()
    {
        session = FindObjectOfType<GameSession>();
        if (!session) {
            sceneLoader.GoToPreload();
        }
        ConfigureRun();
    }

    private void FixedUpdate() {
        UpdateDistanceRemaining();
    }

    private void ConfigureRun() {
        int difficulty = session.ActiveContract.GetContractDifficultyLevel();
        
        // Set remaining distance on UI
        distanceRemaining = session.ActiveContract.GetRunDistance();
        distanceRemainingText.text = distanceRemaining.ToString();

        // Configure spawners with contract difficulty
        ConfigureAsteroidSpawner(difficulty);
        ConfigureEnemySpawner(difficulty);
    }

    private void ConfigureAsteroidSpawner(int difficulty) {
        asteroidSpawner.SetDifficulty(difficulty);
    }

    private void ConfigureEnemySpawner(int difficulty) {
        enemySpanwer.SetDifficulty(difficulty);
    }
    
    private void UpdateDistanceRemaining() {
        if (distanceRemaining > 0) {
            distanceRemaining -= 1;
            distanceRemainingText.text = distanceRemaining.ToString();
        } else {
            session.IsRunSuccessful = true;
            sceneLoader.WaitAndLoadRunResultsScene(2f);
        }
    }
}
