using UnityEngine;

public abstract class EnemyController : LivingInteractable
{
    [Header("Weaponry")]
    [SerializeField] float shotCounter;
    [SerializeField] EnemyWeaponConfig weaponConfig;
    [SerializeField] Transform turret;

    protected Rigidbody2D rigidBody;
    protected EnemyWeapon weapon;

    protected abstract void Move();

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        weapon = (EnemyWeapon)weaponConfig.Create();
        ResetShotCooldown();
    }

    private void Update()
    {
        Move();
        CountDownAndShoot();
    }

    public override void Interact(Interactable other)
    {
        GetComponent<DamageDealer>().DealDamage(other);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            FireWeapon();
            ResetShotCooldown();
        }
    }

    private void FireWeapon()
    {
        weapon.Activate(turret.position, turret.rotation);
    }

    private void ResetShotCooldown()
    {
        float cooldown = weapon.GetCooldown().GetCalcValue();
        float variation = weapon.GetCoolDownVariation();
        shotCounter = Random.Range(cooldown - variation, cooldown + variation);
    }
}
