using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RunResultsLootRow : MonoBehaviour
{
    [SerializeField] private Image lootImage;
    [SerializeField] private TextMeshProUGUI lootName;

    public void DisplayLoot(LootConfig config)
    {
        lootImage.sprite = config.Icon;
        if (config.LootName == "Credits")
        {
            lootName.text = $"{config.LootName} : {((Credits)config).GetCreditValue()}";
        }
        else
        {
            lootName.text = config.LootName;
        }
    }
}
