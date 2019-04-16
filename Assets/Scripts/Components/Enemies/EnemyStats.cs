using UnityEngine;

public class EnemyStats : InteractableStats

{
    [Header("VFX")]
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] float explosionDuration = 1;

    [Header("Audio")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.7f;

    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);
        if (currentHealth <= 0) {
            Die();
        }
    }

    public override void Die() {
        base.Die();

        FindObjectOfType<EnemySpawner>().DecreaseEnemyCount();

        // Death VFX
        ParticleSystem explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);

        // Death SFX
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }
}
