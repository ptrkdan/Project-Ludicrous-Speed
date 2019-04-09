using UnityEngine;

public class Equipment : Interactable 
{
    [SerializeField] EquipmentSlot equipSlot;

    public EquipmentSlot EquipSlot { get => equipSlot; }

    public override void Interact(Interactable other) {
        base.Interact(other);
        EquipmentManager.instance.EquipItem(this);
    }
}

