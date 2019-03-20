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
    [SerializeField] MenuLoader menuLoader;

    [SerializeField] ContractConfig contractConfig;

    private void Start() {
        menuLoader = FindObjectOfType<MenuLoader>();
    }

    private void SetContractTitle(string title) {
        contractTitleText.text = title;
    }

    private void SetLootLevel(int level) {
        for (int i = 0; i < level; i++) {
            Image newLootSprite = Instantiate(lootSprite, lootLevelPanel);
            newLootSprite.GetComponent<Transform>().localPosition 
                = new Vector3(-170 + (i*100), 10 , 0);
        }
    }

    private void SetDifficultyLevel(int level) {
        for (int i = 0; i < level; i++) {
            Image newDifficultySprite = Instantiate(difficultySprite, difficultyLevelPanel);
            newDifficultySprite.GetComponent<Transform>().localPosition
                = new Vector3(-170 + (i * 100), 10, 0);
        }
    }

    public void SetContractConfig(ContractConfig contract) {
        contractConfig = contract;
        SetContractTitle(contract.GetContractTitle());
        SetLootLevel(contract.GetContractLootLevel());
        SetDifficultyLevel(contract.GetContractDifficultyLevel());
    }

    public void SelectContract() {
        menuLoader.OpenContractDetails(contractConfig);
    }
}
