using UnityEngine;

public abstract class EnemyController : LivingInteractable
{
    [Header("Weaponry")]
    [SerializeField] private EnemyWeaponConfig weaponConfig;
    [SerializeField] protected Transform turret;

    [Header("Spawning")]
    [SerializeField] protected SpawnPreference spawnPreference;

    protected Rigidbody2D rigidBody;
    protected EnemyWeapon weapon;

    public SpawnPreference SpawnPreference => spawnPreference;


    protected override void Initialize()
    {
        base.Initialize();

        rigidBody = GetComponent<Rigidbody2D>();

        InitializeWeapon();
        InitializeBehaviours();
    }

    private void InitializeWeapon()
    {
        if (!weaponConfig) return;

        weapon = weaponConfig.Create() as EnemyWeapon;
        weapon.SetTurretPosition(turret);
    }

    private void InitializeBehaviours()
    {
        foreach (Behaviour b in behaviours)
        {
            if (b.GetType().IsSubclassOf(typeof(WeaponBehaviour)))
            {
                WeaponBehaviour offensive = b as WeaponBehaviour;
                offensive.SetWeapon(weapon);
            }
            // Can expand to initialize other types of behaviours
        }
    }

    public override void Interact(Interactable other)
    {
        DamageDealer damageDealer = GetComponent<DamageDealer>();
        if (damageDealer)
        {
            damageDealer.DealDamage(other);
        }
    }
}

public enum SpawnPreference { NearPlayer, Top, Bottom, TopBottom, Middle, Anywhere }