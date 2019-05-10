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
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] BackgroundParticleManager bgParticleManager;
    
    [Header("Spawners")]
    [SerializeField] AsteroidSpawner asteroidSpawner;
    [SerializeField] EnemySpawner enemySpawner;

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI distanceRemainingText;
    [SerializeField] Slider healthBarSlider;

    GameSession session;
    PlayerController player;
    int distanceRemaining;
    float playerEngineStatFactor;

    public void UpdateHealthBar(float currentHealth, float maxHealth) {
        healthBarSlider.value = currentHealth / maxHealth;
    }

    private void Start()
    {
        session = FindObjectOfType<GameSession>();
        if (session == null) {
            sceneLoader.GoToPreload();
        }
        player = FindObjectOfType<PlayerController>();
        player.GetComponent<PlayerStats>().onStatChange += onStatChange;
        ConfigureRun();
    }

    private void FixedUpdate() {
        UpdateDistanceRemaining();
    }

    private void ConfigureRun() {
        // Set remaining distance on UI
        distanceRemaining = session.ActiveContract.GetRunDistance();
        distanceRemainingText.text = distanceRemaining.ToString();

        // Configure spawners with contract difficulty
        int difficulty = session.ActiveContract.GetContractDifficultyLevel();
        ConfigureAsteroidSpawner(difficulty);
        ConfigureEnemySpawner(difficulty);
        ConfigureLootManager();
        
        UpdateSpeed();
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

    private void onStatChange(StatType type)
    {
        if (type == StatType.Engine)
        {
            UpdateSpeed();
        }
    }
    
    private void UpdateSpeed()
    {
        float playerEngineStat = 
            player.GetComponent<PlayerStats>().GetStat(StatType.Engine).GetCalcValue();
        playerEngineStatFactor = playerEngineStat / 20;        // TODO: Create factor formula
        bgParticleManager.AddVelocity(playerEngineStatFactor);
    }

    private void UpdateDistanceRemaining() {
        if (distanceRemaining > 0) {
            distanceRemaining -= (int)(1 + 1 * playerEngineStatFactor);
            distanceRemainingText.text = Mathf.Clamp(distanceRemaining, 0, float.MaxValue).ToString();
        } else {
            session.IsRunSuccessful = true;
            sceneLoader.WaitAndLoadRunResultsScene(2f);
        }
    }
}
