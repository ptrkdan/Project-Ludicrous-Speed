using UnityEngine;


public class WeaponConfig : EquipmentConfig
{
    [Space]
    [SerializeField] private Stat shotCooldown;

    [Header("Projectile Stats")]
    [SerializeField] private Stat damage;
    [SerializeField] private Stat speed;

    [Header("Audio")]
    [SerializeField] private AudioClip shootSFX;
    [SerializeField, Range(0, 1)] private float shootSFXVolume = 0;

    public Stat ShotCooldown { get => shotCooldown; set => shotCooldown = value; }
    public Stat Damage { get => damage; set => damage = value; }
    public Stat Speed { get => speed; set => speed = value; }
    public AudioClip ShootSFX { get => shootSFX; set => shootSFX = value; }
    public float ShootSFXVolume { get => shootSFXVolume; set => shootSFXVolume = value; }
}

