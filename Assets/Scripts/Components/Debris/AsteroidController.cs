using UnityEngine;

public class AsteroidController : LivingInteractable
{
    
    public int Health {
        get => stats.Health;
        set => stats.Health = value; }
    public float MoveSpeed {
        get => stats.speed.GetCalcValue();
    }

    Rigidbody2D rigidBody;

    public override void Interact(Interactable other) {
        GetComponent<DamageDealer>().Interact(other);
    }

    public void SetMoveSpeed(StatModifier mod) {
        stats.speed.AddModifier(mod);
    }

    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        FindObjectOfType<AsteroidSpawner>().IncreaseAsteroidCount();
    }

    private void Update() {
        Move();
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Despawner")) {
            FindObjectOfType<AsteroidSpawner>().DecreaseAsteroidCount();
            Destroy(gameObject);
        } else {
            base.OnTriggerEnter2D(other);
        }
    }

    private void Move() {
        rigidBody.MovePosition(transform.position - Vector3.right * stats.speed.GetCalcValue() * Time.fixedDeltaTime);
    }
}
