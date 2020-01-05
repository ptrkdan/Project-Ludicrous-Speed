using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContractSelection : MonoBehaviour
{

    [Header("Cached")]
    [SerializeField] private GameSession session;
    [SerializeField] private OverlayLoader overlayLoader;
    [SerializeField] private Canvas contractDetailsOverlay;
    [SerializeField] private ContractConfig contract;

    [Header("UI Sprites")]
    [SerializeField] private Image lootSprite;
    [SerializeField] private Image difficultySprite;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI contractTitleText;
    [SerializeField] private Transform lootLevelPanel;
    [SerializeField] private Transform difficultyLevelPanel;

    [Header("UI Offsets")]
    [SerializeField] private float SpriceIconXOffset = -170;
    [SerializeField] private float SpriteIconYOffset = 10;
    [SerializeField] private float SpriteIconWidth = 100;

    private void Start()
    {
        session = FindObjectOfType<GameSession>();
        overlayLoader = FindObjectOfType<OverlayLoader>();
    }

    private void SetContractTitle(string title)
    {
        contractTitleText.text = title;
    }

    private void SetLootLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            Image newLootSprite = Instantiate(lootSprite, lootLevelPanel);
            newLootSprite.GetComponent<Transform>().localPosition
                = new Vector3(SpriceIconXOffset + (i * SpriteIconWidth), SpriteIconYOffset);
        }
    }

    private void SetDifficultyLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            Image newDifficultySprite = Instantiate(difficultySprite, difficultyLevelPanel);
            newDifficultySprite.GetComponent<Transform>().localPosition
                = new Vector3(SpriceIconXOffset + (i * SpriteIconWidth), SpriteIconYOffset);
        }
    }

    public void SetContractConfig(ContractConfig contract)
    {
        this.contract = contract;
        SetContractTitle(contract.ContractTitle);
        SetLootLevel(contract.LootLevel);
        SetDifficultyLevel(contract.DifficultyLevel);
    }

    public void SelectContract()
    {
        session.ActiveContract = contract;
        overlayLoader.OpenContractDetailsOverlay();
    }
}
