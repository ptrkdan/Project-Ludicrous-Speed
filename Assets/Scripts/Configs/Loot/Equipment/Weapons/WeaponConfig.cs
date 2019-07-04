using UnityEngine;


public class WeaponConfig : EquipmentConfig
{
    [Space]
    [SerializeField] Stat shotCooldown;

    [Header("Projectile Stats")]
    [SerializeField] Stat damage;
    [SerializeField] Stat speed;

    [Header("Audio")]
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSFXVolume = 0;


    public Stat ShotCooldown { get => shotCooldown; set => shotCooldown = value; }
    public Stat Damage { get => damage; set => damage = value; }
    public Stat Speed { get => speed; set => speed = value; }
    public AudioClip ShootSFX { get => shootSFX; set => shootSFX = value; }
    public float ShootSFXVolume { get => shootSFXVolume; set => shootSFXVolume = value; }
}

