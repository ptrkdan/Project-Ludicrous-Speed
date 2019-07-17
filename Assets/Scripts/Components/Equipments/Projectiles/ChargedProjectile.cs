public class ChargedProjectile : Projectile
{
    protected bool isCharged = false;

    public bool IsCharged => isCharged;

    public virtual void Dissipate()
    {
        Destroy(gameObject);
    }

    public virtual void DisableCollider() { }

    public virtual void EnableCollider() { }
}
