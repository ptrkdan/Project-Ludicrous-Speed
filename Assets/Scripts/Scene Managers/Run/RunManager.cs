using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
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
    [SerializeField] PlayableDirector runEndTimeline;

    [Header("Spawners")]
    [SerializeField] AsteroidSpawner asteroidSpawner;
    [SerializeField] GameObject securitySpawnerParent;

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI distanceRemainingText;
    [SerializeField] Slider healthBarSlider;

    GameSession session;
    PlayerController player;
    int distanceRemaining;
    bool isFinished = false;
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
        if (!isFinished)
        {
            UpdateDistanceRemaining();
        }
    }

    public void StartRun()
    {
        asteroidSpawner.gameObject.SetActive(true);
        securitySpawnerParent.gameObject.SetActive(true);
    }


    #region Run Configuration
    private void ConfigureRun() {
        // Set remaining distance on UI
        distanceRemaining = session.ActiveContract.GetRunDistance();
        distanceRemainingText.text = distanceRemaining.ToString();

        // Configure spawners with contract difficulty
        int difficulty = session.ActiveContract.GetContractDifficultyLevel();
        ConfigureAsteroidSpawner(difficulty);
        ConfigureEnemySpawner(difficulty);
        ConfigureLootManager();
        ConfigureStats();
    }

    private void ConfigureAsteroidSpawner(int difficulty) {
        asteroidSpawner.SetDifficulty(difficulty);
    }

    private void ConfigureEnemySpawner(int difficulty) {
        SecuritySpawner[] spawners = securitySpawnerParent.GetComponentsInChildren<SecuritySpawner>();
        foreach (SecuritySpawner spawner in spawners)
        {
            spawner.SetDifficulty(difficulty);
        }
    }

    private void ConfigureLootManager()
    {
        LootManager.instance.ConfigureAvailableLoot(
            session.ActiveContract.GetAvailablePickUps(),
            session.ActiveContract.GetAvailablePickUpDropRates());
    }
    #endregion

    private void onStatChange(StatType type)
    {
        if (type == StatType.Engine)
        {
            UpdateSpeed();
        }
    }

    private void ConfigureStats()
    {
        UpdateSpeed();
    }


    private void UpdateSpeed()
    {
        float playerEngineStat = 
            player.GetComponent<PlayerStats>().GetStat(StatType.Engine).GetCalcValue();
        playerEngineStatFactor = playerEngineStat / 25;        // TODO: Create factor formula
        bgParticleManager.UpdateVelocity(playerEngineStatFactor);
    }

    private void UpdateDistanceRemaining() {
        if (distanceRemaining > 0) {
            distanceRemaining -= Mathf.RoundToInt(1 + 1 * playerEngineStatFactor);
            distanceRemainingText.text = Mathf.Clamp(distanceRemaining, 0, float.MaxValue).ToString();
        } else
        {
            isFinished = true;
            session.IsRunSuccessful = true;
            EndRun();
        }
    }

    private void EndRun()
    {
        // Disable player controllers
        player.DisableControls();

        // Move player to starting point
        //player.MoveToStartPosition();
        // Stop spawners
        
        // Play Cutscene
        PlayRunEndCutscene();
    }

    private void PlayRunEndCutscene()
    {
        runEndTimeline.Play();
    }

    public void LoadRunResultsScene()
    {
            sceneLoader.WaitAndLoadRunResultsScene(0f);
    }
}
