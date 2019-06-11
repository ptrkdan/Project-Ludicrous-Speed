﻿using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Loot/Generic Loot")]
// [InitializeOnLoad]
public class LootConfig : ScriptableObject
{
    [SerializeField] string lootName;
    [SerializeField] LootType lootType;
    [SerializeField] Sprite icon;
    [SerializeField] int creditValue = 0;
    [SerializeField] [TextArea (3,5)] string lootDescription;

    public string LootName { get => lootName; }
    public LootType LootType { get => lootType; }
    public string LootDescription { get => lootDescription; }
    public Sprite Icon { get => icon; }
    public int CreditValue { get => creditValue; }

    public virtual Loot Create()
    {
        return new Loot(this);
    }
}
