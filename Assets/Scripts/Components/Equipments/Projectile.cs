using UnityEngine;

[RequireComponent(typeof(DamageDealer))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;

    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject);
    }

    public abstract Projectile Fire();
    public abstract Projectile WithDamage(int value);
    public abstract Projectile WithSpeed(float value);

}
