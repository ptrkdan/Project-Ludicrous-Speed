using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CareerView : Overlay
{
    [Header("UI References")]
    [SerializeField] private Image careerIcon;
    [SerializeField] private TextMeshProUGUI playerTitle;
    [SerializeField] private TextMeshProUGUI playerTitleDescription;

    public void SetCareerIcon(Sprite sprite)
    {
        careerIcon.sprite = sprite;
    }

    public void SetPlayerTitle(string title)
    {
        playerTitle.text = title;
    }

    public void SetPlayerTitleDescription(string description)
    {
        playerTitleDescription.text = description;
    }
}
