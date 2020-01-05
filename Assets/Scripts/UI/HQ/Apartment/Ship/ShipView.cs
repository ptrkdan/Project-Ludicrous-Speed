using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipView : Overlay
{

    public void GoToHangar()
    {
        FindObjectOfType<OverlayLoader>().OpenHangarOverlay();
    }
}
