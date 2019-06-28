using System;
using System.Collections.Generic;

[System.Serializable]
public class EquipmentData
{
    public List<int> equipments;
    public List<float[]> equipmentStats;

    public EquipmentData(EquipmentManager manager)
    {
        equipments = new List<int>();
        equipmentStats = new List<float[]>();
        foreach (EquipmentSlot slot in Enum.GetValues(typeof(EquipmentSlot)))
        {
            Equipment equipment = manager.GetEquipment(slot);
            if (equipment)
            {
                equipments.Add(equipment.GetItemDictKey());

                float[] stats = new float[Enum.GetValues(typeof(StatType)).Length];
                foreach (StatType type in Enum.GetValues(typeof(StatType)))
                {
                    stats[(int)type] = equipment.GetStatModValue(type);
                }
                equipmentStats.Add(stats);
            }
        }
    }
}
