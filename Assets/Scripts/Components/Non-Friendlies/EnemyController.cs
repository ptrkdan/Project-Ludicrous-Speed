using UnityEngine;

public abstract class EnemyController : LivingInteractable
{
    [Header("Weaponry")]
    [SerializeField] float shotCounter;
    [SerializeField] EnemyWeaponConfig weaponConfig;
    [SerializeField] protected Transform turret;

    [Header("Spawning")]
    [SerializeField] protected SpawnPreference spawnPreference;


    protected Rigidbody2D rigidBody;
    protected EnemyWeapon weapon;

    public SpawnPreference GetSpawnPreference() => spawnPreference;
    protected abstract void Move();

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        Move();
        CountDownAndShoot();
    }

    protected override void Initialize()
    {
        base.Initialize();

        rigidBody = GetComponent<Rigidbody2D>();
        if (!weaponConfig) return;

        weapon = (EnemyWeapon)weaponConfig.Create();
        weapon.SetTurretPosition(turret);
        ResetShotCooldown();
    }

    public override void Interact(Interactable other)
    {
        GetComponent<DamageDealer>()?.DealDamage(other);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected virtual void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            FireWeapon();
            ResetShotCooldown();
        }
    }

    protected virtual void FireWeapon()
    {
        weapon.Activate();
    }

    protected virtual void ResetShotCooldown()
    {
        float cooldown = weapon.GetShotCooldown().GetCalcValue();
        float variation = weapon.GetCoolDownVariation();
        shotCounter = Random.Range(cooldown - variation, cooldown + variation);
    }
}

public enum SpawnPreference { NearPlayer, TopBottom, Middle, Anywhere }