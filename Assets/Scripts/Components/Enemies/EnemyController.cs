using UnityEngine;

public abstract class EnemyController : LivingInteractable 
{
    [Header("Weaponry")]
    [SerializeField] float shotCounter; 
    [SerializeField] EnemyWeaponConfig weaponConfig;
    
    protected Rigidbody2D rigidBody;
    protected EnemyWeapon weapon;

    protected abstract void Move();

    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        weapon = (EnemyWeapon) weaponConfig.Create();
        FindObjectOfType<EnemySpawner>().IncreaseEnemyCount();

        ResetShotCooldown();
    }

    private void Update() {
        Move();
        CountDownAndShoot();
    }

    public override void Interact(Interactable other)
    {
        GetComponent<DamageDealer>().DealDamage(other);
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
        weapon.Interact(transform.position, Quaternion.AngleAxis(90, Vector3.forward));
    }

    private void ResetShotCooldown() {
        float cooldown = weapon.GetCooldown().GetCalcValue();
        float variation = weapon.GetCoolDownVariation();
        shotCounter = Random.Range(cooldown - variation, cooldown + variation);
    }
}
