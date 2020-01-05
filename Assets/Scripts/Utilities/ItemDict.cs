using System.Collections.Generic;
using UnityEngine;

public static class ItemDict
{
    private static Dictionary<int, LootConfig> dict = new Dictionary<int, LootConfig>();
    private static int itemKey = 0;

    public static void LoadAllItems()
    {
        itemKey = 0;
        dict.Clear();
        Debug.Log("Loading items in dictionary...");
        LootConfig[] configs = Resources.LoadAll<LootConfig>("Loot Configs");

        foreach (LootConfig config in configs)
        {
            dict.Add(itemKey, config);
            config.ItemDictKey = itemKey;
            itemKey++;
        }
    }

    public static LootConfig GetItem(int key)
    {
        if (dict.Count == 0)
        {
            LoadAllItems();
        }
        return dict[key];
    }
}
