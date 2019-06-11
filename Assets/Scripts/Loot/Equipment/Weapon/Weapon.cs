using UnityEngine;

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