using UnityEngine;

public class AsteroidStats : InteractableStats
{
    [Header("VFX")]
    [SerializeField] private ParticleSystem deathVFXPrefab;
    [SerializeField] private float deathVFXDuration;

    [Header("SFX")]
    [SerializeField] private AudioClip deathSFX;
    [SerializeField, Range(0, 1)] private float deathSFXVolume;

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

        ParticleSystem deathVFX
            = Instantiate(deathVFXPrefab, transform.position, transform.rotation);
        Destroy(deathVFX.gameObject, deathVFXDuration);

        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }
}
