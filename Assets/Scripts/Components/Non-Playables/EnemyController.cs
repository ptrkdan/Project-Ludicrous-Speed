using UnityEngine;

public abstract class EnemyController : LivingInteractable
{
    [Header("Weaponry")]
    [SerializeField] EnemyWeaponConfig weaponConfig;
    [SerializeField] protected Transform turret;

    [Header("Spawning")]
    [SerializeField] protected SpawnPreference spawnPreference;

    protected Rigidbody2D rigidBody;
    protected EnemyWeapon weapon;

    public SpawnPreference GetSpawnPreference() => spawnPreference;
    

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
        weapon = (EnemyWeapon)weaponConfig.Create();
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
        GetComponent<DamageDealer>()?.DealDamage(other);
    }
}

public enum SpawnPreference { NearPlayer, TopBottom, Middle, Anywhere }