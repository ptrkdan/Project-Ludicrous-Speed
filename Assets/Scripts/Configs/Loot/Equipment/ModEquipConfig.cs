using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Equipment/Mod")]
public class ModEquipConfig : EquipmentConfig
{
    public override Loot Create(bool isDefault)
    {
        return new ModEquipment(this, isDefault);
    }

    public override Loot Create()
    {
        return new ModEquipment(this);
    }
}