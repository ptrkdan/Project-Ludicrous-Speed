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

public class ModEquipment : Equipment
{
    public ModEquipment() : base() { }

    public ModEquipment(ModEquipConfig config, bool isDefault = false) 
        : base (config, isDefault)
    {

    }
}