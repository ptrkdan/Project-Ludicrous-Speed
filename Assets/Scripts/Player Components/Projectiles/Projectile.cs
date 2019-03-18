using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected ProjectileConfig config;

    public ProjectileConfig Config { get => config; set => config = value; }

    public abstract void Fire();

    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject);
    }
}
