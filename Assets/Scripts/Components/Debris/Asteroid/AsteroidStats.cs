using UnityEngine;

public class AsteroidStats : InteractableStats
{
    [Header("Audio")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume;

    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);
        if (currentHealth <= 0) {
            Die();
        }
    }

    public override void Die() {
        base.Die();

        DropLoot();

        // TODO: Add explosion vfx

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
