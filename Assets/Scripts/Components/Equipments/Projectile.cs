using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected ProjectileConfig config;

    public ProjectileConfig Config { get => config; set => config = value; }

    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject);
    }

    protected void PlayFireSFX() {
        AudioSource.PlayClipAtPoint(config.ShootSFX, Camera.main.transform.position, config.ShootSFXVolume);
    }

    public abstract void Fire();

}
