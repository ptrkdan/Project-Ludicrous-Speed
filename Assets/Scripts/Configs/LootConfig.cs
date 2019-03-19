using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot Config")]
public class Loot : ScriptableObject
{
    [SerializeField] string lootName;
    [SerializeField] [TextArea] string lootDescription;
    [SerializeField] Sprite lootSprite;
    [SerializeField] int value = 0;

    [Header("Stats")]
    [SerializeField] float damageValue;
    // If equippable, stats like ATK, DEF can be set
    // If credits, stats are not necessary

    public string LootName { get; set; }
    public string LootDescription { get; set; }
    public Sprite LootSprite { get; set; }
    public int Value { get; set; }

}
