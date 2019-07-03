using UnityEngine;

public abstract class Weapon : Equipment
{
    protected WeaponType weaponType;
    protected Transform turret;
    protected Projectile projectilePrefab;
    protected Stat damage;
    protected Stat speed;
    protected Stat cooldown;
    protected AudioClip shootSFX;
    protected float shootSFXVolume;

    public WeaponType GetWeaponType() => weaponType;
    public Projectile GetProjectile() => projectilePrefab;
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
        damage = config.Damage;
        speed = config.Speed;
        shootSFX = config.ShootSFX;
        shootSFXVolume = config.ShootSFXVolume;
    }

    public abstract void SetTurretPosition(Transform turret);
    protected abstract Projectile CreateProjectile();

    protected void PlayFireSFX()
    {
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
    }
}

public enum WeaponType { Auto, Charged }