using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    [Header("Meteor Stats")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] int health = 100;

    [Header("Audio")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume;

    public int Health { get => health; set => health = value; }

    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        FindObjectOfType<MeteorSpawner>().IncreaseMeteorCount();
    }

    // Update is called once per frame
    void Update() {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }

        ProcessHit(damageDealer);
    }

    private void Move() {
        rigidBody.MovePosition(transform.position - Vector3.right * moveSpeed * Time.fixedDeltaTime);
    }

    private void ProcessHit(DamageDealer damageDealer) {
        health -= damageDealer.Damage;

        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        FindObjectOfType<MeteorSpawner>().DecreaseMeteorCount();
        Destroy(gameObject);

        // Add explosion vfx?

        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }
}
