using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;

    public int Damage { get => damage;
        set => damage = value; }

    public void DealDamage(Interactable target) {
        target.GetComponent<LivingInteractable>()?.TakeDamage(damage);
    }
}
