﻿using System;
using System.Collections.Generic;

[Serializable]
public class InventoryData
{
    public int credits;
    public List<int> playerInventory;
    public List<float[]> playerInventoryStats;

    public InventoryData(InventoryManager manager)
    {
        credits = manager.Credits;
        playerInventory = new List<int>();
        playerInventoryStats = new List<float[]>();

        foreach (Loot loot in manager.GetPlayerInventory())
        {
            playerInventory.Add(loot.GetItemDictKey());
            if (loot.GetLootType() == LootType.Equipment)
            {
                Equipment equipment = (Equipment)loot;
                float[] stats = new float[Enum.GetValues(typeof(StatType)).Length];
                foreach (StatType type in Enum.GetValues(typeof(StatType)))
                {
                    stats[(int)type] = equipment.GetStatModValue(type);
                }
                playerInventoryStats.Add(stats);
            }
            else
            {
                playerInventoryStats.Add(null);    // Regular items do not have stats
            }
        }
    }

}
