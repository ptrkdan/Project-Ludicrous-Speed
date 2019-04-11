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

    [SerializeField] EquipmentConfig[] equipments;

    InventoryManager inventory;

    private void Start() {
        inventory = InventoryManager.instance;
        equipments = new EquipmentConfig[System.Enum.GetNames(typeof(EquipmentSlot)).Length];
    }

    public EquipmentConfig GetEquipment(EquipmentSlot slot) {
        return equipments[(int)slot];
    }

    public void Equip(EquipmentConfig newItem) {
        int slotIndex = (int)newItem.EquipSlot;
        EquipmentConfig oldEquipment = equipments[slotIndex];
        if (oldEquipment) {
            inventory.AddToInventory(oldEquipment);
        }
        equipments[slotIndex] = newItem;
        Debug.Log($"<color=green>{newItem.LootName}</color> equipped as <color=green>{newItem.EquipSlot}</color>");
    }
}
