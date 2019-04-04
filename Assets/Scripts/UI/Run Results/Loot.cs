using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Loot : MonoBehaviour
{
    [SerializeField] Image lootImage;
    [SerializeField] TextMeshProUGUI lootName;

    public void DisplayLoot(LootConfig config) {
        lootImage.sprite = config.LootSprite;
        if (config.LootName == "Credits") {
            lootName.text = $"{config.LootName} x {config.LootValue}";
        } else {
            lootName.text = config.LootName;
        }
    }
}
