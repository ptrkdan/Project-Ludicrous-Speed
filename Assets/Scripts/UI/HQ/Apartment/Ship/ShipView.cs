using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipView : OverlayView
{
    MenuLoader menuLoader;

    private void Start() {
        menuLoader = FindObjectOfType<MenuLoader>();
    }

    public void GoToHangar() {
        menuLoader.GoToNextOverlay(menuLoader.HangarCanvas);
    }
}
