using UnityEngine;

public abstract class EnemyController : MonoBehaviour {

    [Header("Stats")]
    [SerializeField] float health = 100;

    [Header("Weaponry")]
    [SerializeField] float shotCounter; 
    [SerializeField] Projectile projectile;

    [Header("VFX")]
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] float explosionDuration = 1;

    [Header("Audio")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.7f;

    protected Rigidbody2D rigidBody;

    protected abstract void Move();

    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        FindObjectOfType<EnemySpawner>().IncreaseEnemyCount();
        ResetShotCooldown();
    }

    private void Update() {
        Move();
        CountDownAndShoot();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Despawner")) {
            FindObjectOfType<EnemySpawner>().DecreaseEnemyCount();
            Destroy(gameObject);
        } else {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }
            ProcessHit(damageDealer);
        }        
    }

    private void CountDownAndShoot() {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0) {
            FireLaser();
            ResetShotCooldown();
        }
    }

    private void FireLaser() {
        Projectile laser = Instantiate(
            projectile, 
            transform.position + projectile.Config.Offset, 
            Quaternion.AngleAxis(90, Vector3.forward));
        laser.Fire();
    }

    private void ResetShotCooldown() {
        shotCounter = UnityEngine.Random.Range(
                    projectile.Config.ShotCooldown - projectile.Config.ShotCooldownVariation,
                    projectile.Config.ShotCooldown + projectile.Config.ShotCooldownVariation);
    }

    private void ProcessHit(DamageDealer damageDealer) {
        health -= damageDealer.Damage;

        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        FindObjectOfType<EnemySpawner>().DecreaseEnemyCount();
        Destroy(gameObject);

        // Death VFX
        ParticleSystem explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);

        // Death SFX
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }
}
