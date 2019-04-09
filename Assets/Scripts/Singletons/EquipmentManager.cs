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

    [SerializeField] Equipment[] equipments 
        = new Equipment[System.Enum.GetNames(typeof(EquipmentSlot)).Length];


    public Equipment GetEquipment(EquipmentSlot slot) {
        return equipments[(int)slot];
    }

    public void EquipItem(Equipment newItem) {
        int slotIndex = (int)newItem.EquipSlot;
        equipments[slotIndex] = newItem;

    }
}
