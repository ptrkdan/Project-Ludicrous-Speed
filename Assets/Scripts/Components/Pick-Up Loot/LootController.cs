using UnityEngine;

public class LootController : Interactable
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rigidBody;
    private LootConfig config;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Despawner"))
        {
            Destroy(gameObject);
        }
        else
        {
            base.OnTriggerEnter2D(other);
        }
    }

    private void Move()
    {
        rigidBody.MovePosition(transform.position - Vector3.right * moveSpeed * Time.fixedDeltaTime);
    }

    public void Drop(LootConfig config, Vector3 position)
    {
        this.config = config;
        transform.position = position;
    }
}
