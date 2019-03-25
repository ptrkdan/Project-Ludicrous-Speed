using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class RunResultsManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] TextMeshProUGUI resultsText;
    [SerializeField] Transform lootPanel;
    [SerializeField] Scrollbar lootPanelScrollbar;
    [SerializeField] Button restartButton;
    [SerializeField] Button hqButton;

    [Header("UI Prefabs")]
    [SerializeField] GameObject lootIcon;

    [Header("UI Offsets")]
    [SerializeField] int lootIconPaddingX = 10;
    [SerializeField] int lootIconPaddingY = 10;
    [SerializeField] int lootIconOffsetY = 70;
    [SerializeField] int lootPanelWidth = 370;
    [SerializeField] int lootPanelHeightPerLoot = 75;

    [Header("Cached")]
    [SerializeField] GameSession session;

    Queue<LootConfig> rewards;

    // Start is called before the first frame update
    void Start()
    {
        session = FindObjectOfType<GameSession>();
        rewards = new Queue<LootConfig>();

        SetResultsText();
        SetLoot();
        
    }

    private void SetResultsText() {
        if(session.IsRunSuccessful) {
            resultsText.text = "Successful!";
            restartButton.gameObject.SetActive(false);
            hqButton.transform.position -= new Vector3(120, 0); // TODO Make into const
        } else {
            resultsText.text = "Busted!";
        }
    }

    private void SetLoot() {
        List<LootConfig> lootList = session.ActiveContract.GetContractRewards();
        for (int i = 0; i < lootList.Count; i++) {
            GameObject newLoot = Instantiate(lootIcon, lootPanel);
            newLoot.transform.Translate(
                lootIconPaddingX, 
                -lootIconPaddingY - (lootIconOffsetY*i), 
                0);
            newLoot.GetComponent<Image>().sprite = lootList[i].LootSprite;
            newLoot.GetComponentInChildren<TextMeshProUGUI>().text = lootList[i].LootName;
        }
        lootPanel.GetComponent<RectTransform>().sizeDelta 
            = new Vector2(lootPanelWidth, lootPanelHeightPerLoot * (lootList.Count));
        lootPanelScrollbar.value = 0; // FIXME: scrollbar still shows up in the middle
    }
}
