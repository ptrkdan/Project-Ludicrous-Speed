using UnityEngine;

public abstract class EnemyController : LivingInteractable 
{
    [Header("Weaponry")]
    [SerializeField] float shotCounter; 
    [SerializeField] EnemyWeaponConfig weapon;
    
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
            FireWeapon();
            ResetShotCooldown();
        }
    }

    private void FireWeapon() {
        weapon.Fire(transform.position, Quaternion.AngleAxis(90, Vector3.forward));
    }

    private void ResetShotCooldown() {
        shotCounter = Random.Range(
                     weapon.Cooldown.GetCalcValue() - weapon.CooldownVariation,
                     weapon.Cooldown.GetCalcValue() + weapon.CooldownVariation);
    }
}
