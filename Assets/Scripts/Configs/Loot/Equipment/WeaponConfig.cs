using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Weapon Config")]
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

    public override void Fire(Vector3 projectileOrigin, Quaternion rotation) {
        base.Fire(projectileOrigin, rotation);
        Projectile projectile = Instantiate(
                ProjectilePrefab,
                projectileOrigin + ProjectileOffset,
                rotation);
        projectile.WithDamage((int)damage.GetCalcValue())
            .WithSpeed(speed.GetCalcValue());
        projectile.Fire();
        PlayFireSFX();
    }

    private void PlayFireSFX() {
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
    }
}