using UnityEngine;

public abstract class EnemyController : LivingInteractable 
{
    [Header("Weaponry")]
    [SerializeField] float shotCounter; 
    [SerializeField] Projectile projectile;
    
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

    protected override void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Despawner")) {
            FindObjectOfType<EnemySpawner>().DecreaseEnemyCount();
            Destroy(gameObject);
        } else {
            base.OnTriggerEnter2D(other);
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
        shotCounter = Random.Range(
                    projectile.Config.ShotCooldown - projectile.Config.ShotCooldownVariation,
                    projectile.Config.ShotCooldown + projectile.Config.ShotCooldownVariation);
    }
}
