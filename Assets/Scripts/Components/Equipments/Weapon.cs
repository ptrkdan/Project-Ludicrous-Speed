using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageDealer))]
public class Weapon : Equipment
{
    [SerializeField] WeaponConfig config;

    public WeaponConfig Config { get => config; }

}
