public class SupportEquipment : Equipment
{
    public SupportEquipment() : base() { }

    public SupportEquipment(SupportEquipConfig config, bool isDefault = false)
        : base(config, isDefault)
    {
        IsDefault = isDefault;
    }
}
