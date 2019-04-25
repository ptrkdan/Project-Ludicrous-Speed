using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CareerView : Overlay
{
    [Header("UI References")]
    [SerializeField] Image careerIcon;
    [SerializeField] TextMeshProUGUI playerTitle;
    [SerializeField] TextMeshProUGUI playerTitleDescription;

    public void SetCareerIcon(Sprite sprite) {
        careerIcon.sprite = sprite;
    }

    public void SetPlayerTitle(string title) {
        playerTitle.text = title;
    }

    public void SetPlayerTitleDescription(string description) {
        playerTitleDescription.text = description;
    }
}
