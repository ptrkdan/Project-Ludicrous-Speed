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

    [Header("Default Equipment")]
    [SerializeField] WeaponConfig primaryWeapon;
    [SerializeField] WeaponConfig secondaryWeapon;
    [SerializeField] SupportEquipConfig supportEquipment;
    [SerializeField] ModEquipConfig primaryMod;
    [SerializeField] ModEquipConfig secondaryMod;

    public delegate void OnEquipmentChanged(Equipment newEquip, Equipment oldEquip);
    public OnEquipmentChanged onEquipmentChanged;

    [SerializeField] Equipment[] currentEquipment =
        new Equipment[System.Enum.GetNames(typeof(EquipmentSlot)).Length];

    InventoryManager inventory;

    private void Start() {
        inventory = InventoryManager.instance;
    }

    public Equipment GetEquipment(EquipmentSlot slot) {
        Equipment equipment = currentEquipment[(int)slot];
        if (equipment == null)
        {
            return GetDefaultEquipment(slot);
        }
        return equipment;
    }

    public void Equip(Equipment newEquip) {
        int slotIndex = (int)newEquip.GetEquipSlot();
        Equipment oldEquip = currentEquipment[slotIndex];
        if (oldEquip) {
            inventory.AddToInventory(oldEquip);
            oldEquip.IsEquipped = false;
        }
        currentEquipment[slotIndex] = newEquip;
        newEquip.IsEquipped = true;

        if (onEquipmentChanged != null) {
            onEquipmentChanged.Invoke(newEquip, oldEquip);
        }

        Debug.Log($"<color=green>{newEquip.GetName()}</color> equipped as <color=green>{newEquip.GetEquipSlot()}</color>");
    }

    public void UnEquip(int slotIndex) {
        Equipment oldEquip = currentEquipment[slotIndex];
        if (oldEquip) {
            inventory.AddToInventory(currentEquipment[slotIndex]);
            currentEquipment[slotIndex] = null;
            oldEquip.IsEquipped = false;
            
            if(onEquipmentChanged != null) {
                onEquipmentChanged.Invoke(null, oldEquip);
            }
        }
    }

    public void UnEquipAll() {
        for (int i = 0; i < currentEquipment.Length; i++) {
            UnEquip(i);
        }
    }

    private Equipment GetDefaultEquipment(EquipmentSlot slot)
    {
        Equipment equipment;
        switch(slot)
        {
            case (EquipmentSlot.PrimaryWeapon):
                equipment = (Weapon) primaryWeapon.Create();
                break;
            case (EquipmentSlot.SecondaryWeapon):
                equipment = (Weapon)secondaryWeapon.Create();
                break;
            case (EquipmentSlot.Support):
            case (EquipmentSlot.PrimaryMod):
            case (EquipmentSlot.SecondaryMod):
            default: return null;
        }
        Equip(equipment);
        return equipment;
    }
}
