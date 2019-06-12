using UnityEngine;

[RequireComponent(typeof(DamageDealer))]
public abstract class Projectile : Interactable
{
    const string PROJECTILE_PARENT_NAME = "Projectiles";

    protected float speed;

    private void Awake()
    {
        GameObject projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);
        if(!projectileParent)
        {
            projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
        }

        transform.parent = projectileParent.transform;
    }

    public override void Interact(Interactable other)
    {
        GetComponent<DamageDealer>().DealDamage(other);
    }
    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);
        Destroy(gameObject);
    }

    public virtual Projectile Fire() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);

        return this;
    }

    public virtual void SetDamage(int value) {
        GetComponent<DamageDealer>().Damage = value;
    }

    public virtual void SetSpeed(float value) {
        speed = value;
    }
}
