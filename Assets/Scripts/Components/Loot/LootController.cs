using UnityEngine;

public class LootController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    Rigidbody2D rigidBody;
    LootConfig config;

    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Move();
    }

    private void Move() {
        rigidBody.MovePosition(transform.position - Vector3.right * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
            Destroy(gameObject);
    }

    public void Drop(LootConfig config, Vector3 position) {
        this.config = config;
        transform.position = position;
    }
}
