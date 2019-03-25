﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot Config")]
public class LootConfig : ScriptableObject
{
    [SerializeField] string lootName;
    [SerializeField] [TextArea (3,5)] string lootDescription;
    [SerializeField] Sprite lootSprite;
    [SerializeField] int lootValue = 0;

    [Header("Stats")]
    [SerializeField] float damageValue;
    // If equippable, stats like ATK, DEF can be set
    // If credits, stats are not necessary

    public string LootName { get => lootName; set => lootName = value; }
    public string LootDescription { get => lootDescription; set => lootDescription = value; }
    public Sprite LootSprite { get => lootSprite; set => lootSprite = value; }
    public int LootValue { get => lootValue; set => lootValue = value; }

}
