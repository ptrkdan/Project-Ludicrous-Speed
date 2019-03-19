using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContractSelection : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI contractTitleText;
    [SerializeField] Transform lootLevelPanel;
    [SerializeField] Transform difficultyLevelPanel;
    [SerializeField] Image lootSprite;
    [SerializeField] Image difficultySprite;


    public void SetContractTitle(string title) {
        contractTitleText.text = title;
    }

    // FIXME
    public void SetLootLevel(int level) {
        for (int i = 0; i < level; i++) {
            Image newLootSprite = Instantiate(lootSprite, lootLevelPanel);
            newLootSprite.GetComponent<Transform>().localPosition 
                = new Vector3(-170 + (i*100), 10 , 0);
        }
    }

    // FIXME
    public void SetDifficultyLevel(int level) {
        for (int i = 0; i < level; i++) {
            Image newDifficultySprite = Instantiate(difficultySprite, difficultyLevelPanel);
            newDifficultySprite.GetComponent<Transform>().localPosition
                = new Vector3(-170 + (i * 100), 10, 0);
        }
    }
}
