using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    [SerializeField] Slider healthBarSlider;
    [SerializeField] bool isInvincible = false;

    [Header("VFX")]
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] float explosionDuration = 1;

    [Header("Audio")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 1f;

    [Header("Misc.")]
    [SerializeField] float gameOverDelay = 2f;


    public override void TakeDamage(int damage) {

        if (!isInvincible) {
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

    private void UpdateHealthBar() {
        healthBarSlider.value = (float)currentHealth / maxHealth;
    }

    public override void Die() {
        base.Die();

        // Death VFX
        ParticleSystem explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);

        // Death SFX
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);


        // Load Run Result Scene
        FindObjectOfType<GameSession>().IsRunSuccessful = false;
        FindObjectOfType<SceneLoader>().WaitAndLoadRunResultsScene(gameOverDelay);
    }

    public void ToggleGodMode() {
        isInvincible = !isInvincible;
    }
}
