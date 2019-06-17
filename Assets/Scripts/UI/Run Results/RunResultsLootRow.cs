using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RunResultsLootRow : MonoBehaviour
{
    [SerializeField] Image lootImage;
    [SerializeField] TextMeshProUGUI lootName;

    public void DisplayLoot(LootConfig config) {
        lootImage.sprite = config.Icon;
        if (config.LootName == "Credits") {
            lootName.text = $"{config.LootName} : {((Credits)config).GetCreditValue()}";
        } else {
            lootName.text = config.LootName;
        }
    }
}
