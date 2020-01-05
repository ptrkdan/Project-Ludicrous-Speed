using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Projectile Config")]
public class ProjectileConfig : ScriptableObject
{
    [Header("Prefab")]
    [SerializeField] private Projectile projectile;

    [Header("Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float shotCooldown;
    [SerializeField] private float shotCooldownVariation;

    [Header("Audio")]
    [SerializeField] private AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] private float shootSFXVolume = 0.3f;

    public Projectile Projectile { get => projectile; set => projectile = value; }
    public float Speed { get => speed; set => speed = value; }
    public float ShotCooldown { get => shotCooldown; set => shotCooldown = value; }
    public float ShotCooldownVariation { get => shotCooldownVariation; set => shotCooldownVariation = value; }
    public AudioClip ShootSFX { get => shootSFX; set => shootSFX = value; }
    public float ShootSFXVolume { get => shootSFXVolume; set => shootSFXVolume = value; }
}
