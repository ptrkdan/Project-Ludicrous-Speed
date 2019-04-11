using UnityEngine;

[RequireComponent(typeof(DamageDealer))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;

    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject);
    }

    public virtual Projectile Fire() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);

        return this;
    }

    public virtual Projectile WithDamage(int value) {
        GetComponent<DamageDealer>().Damage = value;
        return this;
    }

    public virtual Projectile WithSpeed(float value) {
        speed = value;
        return this;
    }
}
