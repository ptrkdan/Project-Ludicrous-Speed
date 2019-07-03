using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float damage = 100;

    public float Damage { get => damage;
        set => damage = value; }

    public void DealDamage(Interactable target) {
        target.GetComponent<LivingInteractable>()?.TakeDamage(damage);
    }
}
