using UnityEngine;

public class AsteroidController : LivingInteractable
{
    Rigidbody2D rigidBody;

    public int GetCurrentHealth() {
        return stats.GetCurrentHealth();
    }

    public void SetCurrentHealth(int currentHealth) {
        stats.SetCurrentHealth(currentHealth);
    }

    public float GetMoveSpeed() {
        return stats.GetStat(StatType.Engine).GetCalcValue();
    }

    public void SetMoveSpeed(StatModifier mod) {
        stats.GetStat(StatType.Engine).AddModifier(mod);
    }

    public override void Interact(Interactable other) {
        GetComponent<DamageDealer>().Interact(other);
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
        rigidBody.MovePosition(transform.position - Vector3.right * GetMoveSpeed() * Time.fixedDeltaTime);
    }
}
