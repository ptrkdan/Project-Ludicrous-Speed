using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [Header("Meteor Stats")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] int health = 100;

    [Header("Audio")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume;

    public int Health { get => health; set => health = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    Rigidbody2D rigidBody;

    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        FindObjectOfType<AsteroidSpawner>().IncreaseAsteroidCount();
    }

    private void Update() {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Despawner")) {
            FindObjectOfType<AsteroidSpawner>().DecreaseAsteroidCount();
            Destroy(gameObject);
        } else {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }      // If other object does not have a DamageDealer, ignore collision
            ProcessHit(damageDealer);
        }
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
        FindObjectOfType<AsteroidSpawner>().DecreaseAsteroidCount();
        Destroy(gameObject);

        // TODO: Add explosion vfx

        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }
}
