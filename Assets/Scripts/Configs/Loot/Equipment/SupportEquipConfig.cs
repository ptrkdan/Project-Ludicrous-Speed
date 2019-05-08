using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Equipment/Support")]
public class SupportEquipConfig : EquipmentConfig
{
    public override Loot Create(bool isDefault)
    {
        return new SupportEquipment(this, isDefault);
    }

    public override Loot Create()
    {
        return new SupportEquipment(this);
    }
}

public class SupportEquipment : Equipment
{
    public SupportEquipment() : base() { }

    public SupportEquipment(SupportEquipConfig config, bool isDefault = false) 
        : base(config, isDefault)
    {

    }
}
