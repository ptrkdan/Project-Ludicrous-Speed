using UnityEngine;

public class AsteroidStats : InteractableStats
{
    [Header("VFX")]
    [SerializeField] ParticleSystem deathVFXPrefab;
    [SerializeField] float deathVFXDuration;

    [Header("SFX")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume;

    public override void TakeDamage(float damage) {
        base.TakeDamage(damage);
        if (currentHealth <= 0) {
            Die();
        }
    }

    public override void Die() {
        base.Die();

        DropLoot();

        ParticleSystem deathVFX
            = Instantiate(deathVFXPrefab, transform.position, transform.rotation);
        Destroy(deathVFX.gameObject, deathVFXDuration);
        
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }

    private void DropLoot() {
        PickUpLootConfig lootConfig = LootManager.instance.DropLoot();
        if (lootConfig != null) {
            LootController loot = Instantiate(lootConfig.LootPrefab, gameObject.transform.parent);
            loot.Drop(lootConfig, transform.position);
        }
    }
}
