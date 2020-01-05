using TMPro;
using UnityEngine;

public class CreditsView : Overlay
{
    [SerializeField] private TextMeshProUGUI creditsText;

    private void OnEnable()
    {
        InventoryManager.instance.onPlayerInventoryChangedCallback += UpdateCredits;
        UpdateCredits();
    }

    private void OnDisable()
    {
        InventoryManager.instance.onPlayerInventoryChangedCallback -= UpdateCredits;
    }

    public void UpdateCredits()
    {
        creditsText.text = $"Credits: {InventoryManager.instance.Credits}";
    }
}
