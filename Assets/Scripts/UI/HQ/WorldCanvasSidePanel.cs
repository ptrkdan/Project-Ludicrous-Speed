using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WorldCanvasSidePanel : MonoBehaviour
{
    [SerializeField] Image watchtowerSprite;
    [SerializeField] Image hangarSprite;
    [SerializeField] Image marketSprite;
    [SerializeField] Image officeSprite;
    [SerializeField] Image apartmentSprite;

    [Header("Fade")]
    [SerializeField] float fadeAlpha = 0.7f;
    [SerializeField] float fadeDuration = 0.2f;

    Image targetSprite;
    bool isFadingOut = true;

    private void Update()
    {
        if (targetSprite)
        {
            FadeSprite();
        }
    }

    public void StartFadeWatchtower()
    {
        targetSprite = watchtowerSprite;        
    }

    public void StartFadeHangar()
    {
        targetSprite = hangarSprite;
    }

    public void StartFadeMarket()
    {
        targetSprite = marketSprite;
    }

    public void StartFadeOffice()
    {
        targetSprite = officeSprite;
    }

    public void StartFadeApartment()
    {
        targetSprite = apartmentSprite;
    }

    public void EndFade()
    {
        targetSprite.color = new Color(1, 1, 1, 1);
        targetSprite = null;
    }

    private void FadeSprite()
    {
        if (isFadingOut)
        {
            targetSprite.CrossFadeAlpha(fadeAlpha, fadeDuration, true);
            isFadingOut = false;
        }
        else
        {
            targetSprite.CrossFadeAlpha(1, fadeDuration, true);
            isFadingOut = true;
        }
    }
}
