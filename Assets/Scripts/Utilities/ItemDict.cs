using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class ItemDict
{
    static Dictionary<string, LootConfig> dict = new Dictionary<string, LootConfig>();
    
    [MenuItem("AssetDatabase/LoadAllItems")]
    public static void LoadAllItems()
    {
        dict.Clear();
        Debug.Log("Loading items...");
        LootConfig[] configs = Resources.LoadAll<LootConfig>("Loot Configs");

        foreach (LootConfig config in configs)
        {
            dict.Add(config.LootName, config);
        }
    }

    public static LootConfig GetItem(string name)
    {
        if (dict.Count == 0)
        {
            LoadAllItems();
        }
        return dict[name];
    }
}
