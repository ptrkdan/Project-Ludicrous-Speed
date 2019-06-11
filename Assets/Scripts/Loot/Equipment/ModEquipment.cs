public class ModEquipment : Equipment
{
    public ModEquipment() : base() { }

    public ModEquipment(ModEquipConfig config, bool isDefault = false)
        : base(config, isDefault)
    {
        IsDefault = isDefault;
    }
}
