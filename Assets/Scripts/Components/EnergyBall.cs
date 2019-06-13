using UnityEngine;

public class EnergyBall : ChargedProjectile
{
    float size = 1f;
    [SerializeField] float maxSize = 3f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Create explosion field that will expand in time around point of collision
    }

    public void IncreaseSize(float amount)
    {
        if (size < maxSize)
        {
            size += amount;
            GetComponentInChildren<Transform>().localScale += new Vector3(size, size);
        }
        
        if (size >= maxSize)
        {
            isCharged = true;
        }
    }

    public override void DisableCollider()
    {
        GetComponent<CircleCollider2D>().enabled = false;
    }

    public override void EnableCollider()
    {
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
