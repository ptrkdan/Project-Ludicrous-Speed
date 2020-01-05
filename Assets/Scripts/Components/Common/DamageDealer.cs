using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] public float Damage = 100;

    public void DealDamage(Interactable target)
    {
        if (target is LivingInteractable)
        {
            target.GetComponent<LivingInteractable>().TakeDamage(Damage);
        }
    }
}
