using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Equipment/Weapon")]
public class WeaponConfig : EquipmentConfig
{
    [Header("Projectile")]
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Vector3 projectileOffset;

    [Header("Projectile Stats")]
    [SerializeField] Stat damage;
    [SerializeField] Stat speed;
    [SerializeField] Stat cooldown;

    [Header("Audio")]
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSFXVolume = 0;
    
    public Stat Damage { get => damage; set => damage = value; }
    public Stat Speed { get => speed; set => speed = value; }
    public Stat Cooldown { get => cooldown; set => cooldown = value; }
    public Projectile ProjectilePrefab { get => projectilePrefab; set => projectilePrefab = value; }
    public Vector3 ProjectileOffset { get => projectileOffset; set => projectileOffset = value; }
    public AudioClip ShootSFX { get => shootSFX; set => shootSFX = value; }
    public float ShootSFXVolume { get => shootSFXVolume; set => shootSFXVolume = value; }

    public override Loot Create(bool isDefault)
    {
        return new Weapon(this, isDefault);
    }

    public override Loot Create()
    {
        return new Weapon(this);
    }
}

public class Weapon : Equipment
{
    Projectile projectilePrebab;
    Vector3 projectileOffset;
    Stat damage;
    Stat speed;
    Stat cooldown;
    AudioClip shootSFX;
    float shootSFXVolume;

    public Projectile GetProjectile() => projectilePrebab;
    public Vector3 GetProjectileOffset() => projectileOffset;
    public Stat GetDamage() => damage;
    public Stat GetSpeed() => speed;
    public Stat GetCooldown() => cooldown;
    public AudioClip GetShootSFX() => shootSFX;
    public float GetShootSFXVolume() => shootSFXVolume;

    public Weapon() : base() { }

    public Weapon(WeaponConfig config, bool isDefault = false) 
        : base(config, isDefault)
    {
        IsDefault = isDefault;
        projectilePrebab = config.ProjectilePrefab;
        projectileOffset = config.ProjectileOffset;
        damage = config.Damage;
        speed = config.Speed;
        cooldown = config.Cooldown;
        shootSFX = config.ShootSFX;
        shootSFXVolume = config.ShootSFXVolume;
    }

    public override void Use(Vector3 projectileOrigin, Quaternion rotation)
    {
        base.Use(projectileOrigin, rotation);
        Projectile projectile = Instantiate(
                projectilePrebab,
                projectileOrigin + projectileOffset,
                rotation);
        projectile.WithDamage((int)damage.GetCalcValue())
            .WithSpeed(speed.GetCalcValue());
        projectile.Fire();
        PlayFireSFX();
    }


    private void PlayFireSFX()
    {
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
    }
}