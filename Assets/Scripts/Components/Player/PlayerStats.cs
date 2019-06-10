using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : InteractableStats
{
    [Header("VFX")]
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] float explosionDuration = 1;

    [Header("Audio")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 1f;

    [Header("Misc.")]
    [SerializeField] bool isInvincible = false;
    [SerializeField] float gameOverDelay = 2f;

    private void Start() {
        RetrieveStats();
    }

    private void RetrieveStats()
    {
        maxHealth = StatsManager.instance.GetMaxHealth();
        currentHealth = maxHealth;
        hull = StatsManager.instance.GetStat(StatType.Hull);
        shield = StatsManager.instance.GetStat(StatType.Shield);
        engine = StatsManager.instance.GetStat(StatType.Engine);
        weapon = StatsManager.instance.GetStat(StatType.Weapon);
        aux = StatsManager.instance.GetStat(StatType.Aux);
    }

    public override void TakeDamage(int damage) {
        if (!isInvincible) {
            // Check for shield
            base.TakeDamage(damage);
            UpdateHealthBar();
            if (currentHealth <= 0) {
                Die();
            }
        }
    }

    public override void RepairDamage(int repair) {
        base.RepairDamage(repair);
        UpdateHealthBar();
    }

    public override void Die() {
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

    public void ToggleGodMode() {
        isInvincible = !isInvincible;
    }

    private void UpdateHealthBar() {
        RunManager.instance.UpdateHealthBar((float)currentHealth, (float)maxHealth);
    }
}
