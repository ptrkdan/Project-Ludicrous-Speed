using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class RunManager : MonoBehaviour
{
    #region Singleton
    public static RunManager instance;
    public RunManager()
    {
        if (instance) return;
        instance = this;
    }
    #endregion
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] BackgroundParticleManager bgParticleManager;
    [SerializeField] PlayableDirector runEndTimeline;

    [Header("Spawners")]
    [SerializeField] DebrisSpawner asteroidSpawner;
    [SerializeField] SecuritySpawner securitySpawner;
    [SerializeField] CreatureSpawner leftCreatureSpawner;
    [SerializeField] CreatureSpawner rightCreatureSpawner;

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI distanceRemainingText;

    GameSession session;
    ContractConfig config;
    PlayerController player;

    int distanceRemaining;

    bool isStarted = false;
    bool isFinished = false;
    float playerEngineStatFactor;
    private void Start()
    {
        session = FindObjectOfType<GameSession>();
        if (session == null)
        {
            sceneLoader.GoToPreload();
        }
        config = session.ActiveContract;
        player = FindObjectOfType<PlayerController>();
        player.GetComponent<PlayerStats>().onStatChange += onStatChange;

        ConfigureRun();
    }

    private void FixedUpdate()
    {
        if (!isFinished && isStarted)
        {
            UpdateDistanceRemaining();
        }
    }

    public void StartRun()
    {
        isStarted = true;
        asteroidSpawner.gameObject.SetActive(true);
        securitySpawner.gameObject.SetActive(true);
        leftCreatureSpawner.gameObject.SetActive(true);
        rightCreatureSpawner.gameObject.SetActive(true);
    }

    public void StopSpawners()
    {
        asteroidSpawner.StopSpawning();
        securitySpawner.StopSpawning();
        leftCreatureSpawner.StopSpawning();
        rightCreatureSpawner.StopSpawning();
    }

    public void UpdateHullArmouBar(float value)
    {
        FindObjectOfType<HullArmourSlider>().UpdateValue(value);
    }

    public void UpdateShieldBar(float value)
    {
        FindObjectOfType<ShieldSlider>().UpdateValue(value);
    }
       
    #region Run Configuration
    private void ConfigureRun()
    {
        // Set remaining distance on UI
        distanceRemaining = config.RunDistance;
        distanceRemainingText.text = distanceRemaining.ToString();

        // Configure spawners
        ConfigureAsteroidSpawner();
        ConfigureEnemySpawner();
        ConfigureLootManager();
        ConfigureStats();
    }

    private void ConfigureAsteroidSpawner()
    {
        asteroidSpawner.SetDifficulty(config.DifficultyLevel);
        asteroidSpawner.SetDebrisPrefabs(config.Debris);
    }

    private void ConfigureEnemySpawner()
    {
        securitySpawner.SetDifficulty(config.DifficultyLevel);
        securitySpawner.SetSecurityUnitPrefabs(config.SecurityUnits);
    }

    private void ConfigureLootManager()
    {
        LootManager.instance.ConfigureAvailableLoot(
            config.PickUps);
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

    private void UpdateDistanceRemaining()
    {
        if (distanceRemaining > 0)
        {
            distanceRemaining -= Mathf.RoundToInt(1 + 1 * playerEngineStatFactor);
            distanceRemainingText.text = Mathf.Clamp(distanceRemaining, 0, float.MaxValue).ToString();
        }
        else
        {
            StopSpawners();
            if (IsMapClear())
            {
                isFinished = true;
                session.IsRunSuccessful = true;
                EndRun();
            }
        }
    }

    private bool IsMapClear()
    {
        SecuritySpawnPoint[] securitySpawnPoints = 
            securitySpawner.GetComponentsInChildren<SecuritySpawnPoint>();
        foreach (SecuritySpawnPoint spawnPoint in securitySpawnPoints)
        {
            if (spawnPoint.HasChildren())
            {
                return false;// Spawner has children; map is not cleared
            }      
        }
        AsteroidSpawnPoint[] asteroidSpawnPoints =
            asteroidSpawner.GetComponentsInChildren<AsteroidSpawnPoint>();
        foreach (AsteroidSpawnPoint spawnPoint in asteroidSpawnPoints)
        {
            if (spawnPoint.HasChildren())
            {
                return false;
            }
        }

        return true;    // All spawners have no children
    }

    private void EndRun()
    {
        // Disable player controllers
        player.DisableControls();

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
