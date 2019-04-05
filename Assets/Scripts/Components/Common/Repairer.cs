using UnityEngine;

public class Repairer : PickUp
{
    [SerializeField] int repairValue = 100;

    public int RepairValue { get => repairValue; set => repairValue = value; }
    
    public override void Interact(Interactable target) {
        base.Interact(target);
        if (target.GetType().IsSubclassOf(typeof(LivingInteractable))) {
            Repair((LivingInteractable)target);
        }
    }

    private void Repair(LivingInteractable target) {
        target.RepairDamage(repairValue);
    }
}
