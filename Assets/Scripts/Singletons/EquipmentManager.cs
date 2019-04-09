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

    [SerializeField] EquipmentConfig[] equipments 
        = new EquipmentConfig[System.Enum.GetNames(typeof(EquipmentSlot)).Length];


    public EquipmentConfig GetEquipment(EquipmentSlot slot) {
        return equipments[(int)slot];
    }

    public void EquipItem(EquipmentConfig newItem) {
        int slotIndex = (int)newItem.EquipSlot;
        equipments[slotIndex] = newItem;
        Debug.Log($"<color=green>{newItem.LootName}</color> equipped as <color=green>{newItem.EquipSlot}</color>");
    }
}
