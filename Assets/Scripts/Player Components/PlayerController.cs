using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] float moveSpeed = 30f;
    [SerializeField] int maxHealth = 500;
    [SerializeField] int health = 500;
    [SerializeField] Slider healthBarSlider;

    [Header("Projectile")]
    [SerializeField] Projectile projectile;

    [Header("VFX")]
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] float explosionDuration = 1;

    [Header("Audio")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 1f;

    [Header("Misc.")]
    [SerializeField] float gameOverDelay = 2f;
    [SerializeField] bool isInvincible = true;

    Coroutine fireLaserCoroutine;

    Vector3 movement = new Vector2();

    Rigidbody2D rigidBody;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Fire();
    }

    private void FixedUpdate() {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }

        if (!isInvincible) {
            ProcessHit(damageDealer);
        }
    }

    private void Fire() {
        if (Input.GetButtonDown("Fire1")) {
            fireLaserCoroutine = StartCoroutine(FireLaser());
        }
        if (Input.GetButtonUp("Fire1")) {
            StopCoroutine(fireLaserCoroutine);
        }
    }

    IEnumerator FireLaser() {
        while (true) {
            Projectile laser = Instantiate(
                projectile, 
                transform.position + projectile.Config.Offset,
                Quaternion.AngleAxis(-90, Vector3.forward));
            laser.Fire();

            yield return new WaitForSeconds(projectile.Config.ShotCooldown);
        }
    }

    private void Move() {   
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rigidBody.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void ProcessHit(DamageDealer damageDealer) {
        health -= damageDealer.Damage;
        healthBarSlider.value = (float)health / maxHealth;

        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);

        // Death VFX
        ParticleSystem explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);

        // Death SFX
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);

        FindObjectOfType<SceneLoader>().WaitAndLoadGameOverScene(gameOverDelay);
    }
}
