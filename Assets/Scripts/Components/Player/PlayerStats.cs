using UnityEngine;

public class PlayerStats : InteractableStats
{
    [Header("VFX")]
#pragma warning disable CS0649 // Field 'PlayerStats.explosionVFX' is never assigned to, and will always have its default value null
    [SerializeField] private ParticleSystem explosionVFX;
#pragma warning restore CS0649 // Field 'PlayerStats.explosionVFX' is never assigned to, and will always have its default value null
    [SerializeField] private float explosionDuration = 1;

    [Header("Audio")]
#pragma warning disable CS0649 // Field 'PlayerStats.deathSFX' is never assigned to, and will always have its default value null
    [SerializeField] private AudioClip deathSFX;
#pragma warning restore CS0649 // Field 'PlayerStats.deathSFX' is never assigned to, and will always have its default value null
    [SerializeField] [Range(0, 1)] private float deathSFXVolume = 1f;

    [Header("Misc.")]
    [SerializeField] private bool isInvincible = false;
    [SerializeField] private float gameOverDelay = 2f;
    private float maxShield;
    private float currentShield;
    private float shieldRegenDelay;
    private float shieldRegenAmount;
    private float timeUntilShieldRegen;

    private void Start()
    {
        RetrieveStats();
    }

    private void Update()
    {
        UpdateShieldRegen();
    }

    private void RetrieveStats()
    {
        hull = StatsManager.instance.GetStat(StatType.Hull);
        shield = StatsManager.instance.GetStat(StatType.Shield);
        engine = StatsManager.instance.GetStat(StatType.Engine);
        weapon = StatsManager.instance.GetStat(StatType.Weapon);
        aux = StatsManager.instance.GetStat(StatType.Aux);
        maxHealth = StatsManager.instance.GetMaxHealth();
        currentHealth = maxHealth;
        maxShield = Mathf.FloorToInt(shield.Value) * 100;  // TODO Make shield factor const
        currentShield = maxShield;
        shieldRegenDelay = 200;                                     // TODO Finalize shield regen formula
        shieldRegenAmount = aux.Value;                 // TODO Make shield regen amount formula
        timeUntilShieldRegen = shieldRegenDelay;
    }

    public override void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            float remainingDamage = damage;
            // Check for shield
            if (currentShield > 0)
            {
                ResetShieldRegen();
                currentShield -= damage;
                if (currentShield < 0)
                {
                    remainingDamage = currentShield * -1; // Get the inverse of current shield value
                    currentShield = 0;
                }
                else
                {
                    remainingDamage = 0;
                }
                UpdateShieldBar();
            }

            if (remainingDamage > 0)
            {   // if there is still remaining daamge after shield, take hull damage
                base.TakeDamage(Mathf.FloorToInt(remainingDamage));
                UpdateHullArmouBar();
                if (currentHealth <= 0)
                {
                    Die();
                }
            }
        }
    }

    public override void RepairDamage(float repair)
    {
        base.RepairDamage(repair);
        UpdateHullArmouBar();
    }

    public override void Die()
    {
        base.Die();

        // Death VFX
        ParticleSystem explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(explosion.gameObject, explosionDuration);

        // Death SFX
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);


        // Load Run Result Scene
        FindObjectOfType<GameSession>().IsRunSuccessful = false;
        FindObjectOfType<SceneLoader>().WaitAndLoadRunResultsScene(gameOverDelay);
    }

    public void ToggleGodMode()
    {
        isInvincible = !isInvincible;
    }

    private void ResetShieldRegen()
    {
        timeUntilShieldRegen = shieldRegenDelay;
    }

    private void UpdateShieldRegen()
    {
        if (timeUntilShieldRegen > 0)
        {
            timeUntilShieldRegen -= 1;      // TODO Create shield regen formula
        }
        else
        {
            if (currentShield < maxShield)
            {
                currentShield = Mathf.Clamp(currentShield + Mathf.FloorToInt(shieldRegenAmount), 0, maxShield);
                UpdateShieldBar();
            }
        }

    }

    private void UpdateHullArmouBar()
    {
        RunManager.instance.UpdateHullArmouBar((float)currentHealth / maxHealth);
    }

    private void UpdateShieldBar()
    {
        RunManager.instance.UpdateShieldBar((float)currentShield / maxShield);
    }
}
