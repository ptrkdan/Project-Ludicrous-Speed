using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsView : Overlay
{
    [SerializeField] TextMeshProUGUI creditsText;

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
