using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RunManager : MonoBehaviour
{
    #region Singleton
    public static RunManager instance;
    public RunManager() {
        if (instance) return;
        instance = this;
    }
    #endregion

    [Header("Cached")]
    [SerializeField] GameSession session;
    [SerializeField] SceneLoader sceneLoader;

    [Header("Spawners")]
    [SerializeField] AsteroidSpawner asteroidSpawner;
    [SerializeField] EnemySpawner enemySpawner;

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI distanceRemainingText;
    [SerializeField] Slider healthBarSlider;

    private int distanceRemaining;

    public void UpdateHealthBar(float currentHealth, float maxHealth) {
        healthBarSlider.value = currentHealth / maxHealth;
    }

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
        ConfigureLootManager();
        
    }

    private void ConfigureAsteroidSpawner(int difficulty) {
        asteroidSpawner.SetDifficulty(difficulty);
    }

    private void ConfigureEnemySpawner(int difficulty) {
        enemySpawner.SetDifficulty(difficulty);
    }

    private void ConfigureLootManager()
    {
        LootManager.instance.ConfigureAvailableLoot(
            session.ActiveContract.GetAvailablePickUps(),
            session.ActiveContract.GetAvailablePickUpDropRates());
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
