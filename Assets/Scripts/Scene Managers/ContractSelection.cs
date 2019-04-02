using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContractSelection : MonoBehaviour
{

    [Header("Cached")]
    [SerializeField] GameSession session;
    [SerializeField] MenuLoader menuLoader;
    [SerializeField] Canvas contractDetailsOverlay;
    [SerializeField] ContractConfig contract;

    [Header("UI Sprites")]
    [SerializeField] Image lootSprite;
    [SerializeField] Image difficultySprite;

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI contractTitleText;
    [SerializeField] Transform lootLevelPanel;
    [SerializeField] Transform difficultyLevelPanel;

    [Header("UI Offsets")]
    [SerializeField] float SpriceIconXOffset = -170;
    [SerializeField] float SpriteIconYOffset = 10;
    [SerializeField] float SpriteIconWidth = 100;

    private void Start() {
        session = FindObjectOfType<GameSession>();
        menuLoader = FindObjectOfType<MenuLoader>();
    }

    private void SetContractTitle(string title) {
        contractTitleText.text = title;
    }

    private void SetLootLevel(int level) {
        for (int i = 0; i < level; i++) {
            Image newLootSprite = Instantiate(lootSprite, lootLevelPanel);
            newLootSprite.GetComponent<Transform>().localPosition 
                = new Vector3(SpriceIconXOffset + (i * SpriteIconWidth), SpriteIconYOffset);
        }
    }

    private void SetDifficultyLevel(int level) {
        for (int i = 0; i < level; i++) {
            Image newDifficultySprite = Instantiate(difficultySprite, difficultyLevelPanel);
            newDifficultySprite.GetComponent<Transform>().localPosition
                = new Vector3(SpriceIconXOffset + (i * SpriteIconWidth), SpriteIconYOffset);
        }
    }

    public void SetContractConfig(ContractConfig contract) {
        this.contract = contract;
        SetContractTitle(contract.GetContractTitle());
        SetLootLevel(contract.GetContractLootLevel());
        SetDifficultyLevel(contract.GetContractDifficultyLevel());
    }

    public void SelectContract() {
        session.ActiveContract = contract;
        menuLoader.OpenContractDetails();
    }
}
