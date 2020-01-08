using UnityEngine;

public class EnergyBallExplosion : Explosion
{
    private void OnTriggerStay2D(Collider2D other)
    {
        Interactable target = other.GetComponent<Interactable>();
        if (target)
        {
            Interact(target);
        }
    }

    public override void Interact(Interactable other)
    {
        base.Interact(other);
        GetComponent<DamageDealer>().DealDamage(other);
    }

    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}
