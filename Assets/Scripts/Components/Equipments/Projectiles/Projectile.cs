using UnityEngine;

[RequireComponent(typeof(DamageDealer))]
public class Projectile : Interactable
{
    const string PROJECTILE_PARENT_NAME = "Projectiles";

    protected Vector2 velocity;

    private void Awake()
    {
        AssignProjectileTransformParent();
    }

    public override void Interact(Interactable other)
    {
        GetComponent<DamageDealer>().DealDamage(other);
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);
        Destroy(gameObject);        // May not be the the case for all projectiles
    }

    public virtual void Fire() {
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    public virtual void SetDamage(float value) {
        GetComponent<DamageDealer>().Damage = value;
    }

    public virtual void SetVelocity(Vector2 value) {
        velocity = value;
    }

    private void AssignProjectileTransformParent()
    {
        GameObject projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);
        if (!projectileParent)
        {
            projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
        }

        transform.parent = projectileParent.transform;
    }
}
