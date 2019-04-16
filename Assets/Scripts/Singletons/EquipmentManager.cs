using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;
    private void Awake() {
        if (instance) {
            return;   
        }
        instance = this;
    }
    #endregion

    public delegate void OnEquipmentChanged(EquipmentConfig newEquip, EquipmentConfig oldEquip);
    public OnEquipmentChanged onEquipmentChanged;

    [SerializeField] EquipmentConfig[] equipment =
        new EquipmentConfig[System.Enum.GetNames(typeof(EquipmentSlot)).Length];

    InventoryManager inventory;

    private void Start() {
        inventory = InventoryManager.instance;
    }

    public EquipmentConfig GetEquipment(EquipmentSlot slot) {
        return equipment[(int)slot];
    }

    public void Equip(EquipmentConfig newEquip) {
        int slotIndex = (int)newEquip.EquipSlot;
        EquipmentConfig oldEquip = equipment[slotIndex];
        if (oldEquip) {
            inventory.AddToInventory(oldEquip);
        }
        equipment[slotIndex] = newEquip;

        if (onEquipmentChanged != null) {
            onEquipmentChanged.Invoke(newEquip, oldEquip);
        }
        Debug.Log($"<color=green>{newEquip.LootName}</color> equipped as <color=green>{newEquip.EquipSlot}</color>");
    }

    public void UnEquip(int slotIndex) {
        EquipmentConfig oldEquip = equipment[slotIndex];
        if (oldEquip) {
            inventory.AddToInventory(equipment[slotIndex]);
            equipment[slotIndex] = null;
            
            if(onEquipmentChanged != null) {
                onEquipmentChanged.Invoke(null, oldEquip);
            }
        }
    }

    public void UnEquipAll() {
        for (int i = 0; i < equipment.Length; i++) {
            UnEquip(i);
        }
    }
}
