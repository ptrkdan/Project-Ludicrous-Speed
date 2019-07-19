using UnityEngine;

public abstract class Weapon : Equipment
{
    protected WeaponType weaponType;
    protected Transform turret;
    protected Projectile projectilePrefab;
    protected Stat damage;
    protected Stat speed;
    protected Stat shotCooldown;
    protected AudioClip shootSFX;
    protected float shootSFXVolume;

    public WeaponType GetWeaponType() => weaponType;
    public Projectile GetProjectile() => projectilePrefab;
    public Stat GetDamage() => damage;
    public Stat GetSpeed() => speed;
    public Stat GetShotCooldown() => shotCooldown;
    public AudioClip GetShootSFX() => shootSFX;
    public float GetShootSFXVolume() => shootSFXVolume;

    public Weapon() : base() { }

    public Weapon(WeaponConfig config, bool isDefault = false)
        : base(config, isDefault)
    {
        IsDefault = isDefault;
        shotCooldown = config.ShotCooldown;
        damage = config.Damage;
        speed = config.Speed;
        shootSFX = config.ShootSFX;
        shootSFXVolume = config.ShootSFXVolume;
    }
    public Transform GetTurretPosition() => turret;
    public abstract void SetTurretPosition(Transform turret);
    protected abstract Projectile CreateProjectile();

    protected void PlayFireSFX()
    {
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
    }
}

public enum WeaponType { Auto, SemiAuto, Burst, Charged }