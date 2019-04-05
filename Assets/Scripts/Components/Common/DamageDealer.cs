using UnityEngine;

public class DamageDealer : Interactable
{
    [SerializeField] int damage = 100;

    public int Damage { get => damage; set => damage = value; }

    public override void Interact(Interactable target) {
        base.Interact(target);
        if (target.GetType().IsSubclassOf(typeof(LivingInteractable))) { 
            Hit((LivingInteractable) target);
        }
    }

    private void Hit(LivingInteractable target) {
        target.TakeDamage(damage);
    }
}
