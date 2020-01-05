using UnityEngine;

public class EnemyStats : InteractableStats

{
    [Header("VFX")]
    [SerializeField] private ParticleSystem explosionVFX;
    [SerializeField] private float explosionDuration = 1f;

    [Header("Audio")]
    [SerializeField] private AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] private float deathSFXVolume = 0.7f;

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        base.Die();

        // Death VFX
        ParticleSystem explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(explosion.gameObject, explosionDuration);

        // Death SFX
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }
}
