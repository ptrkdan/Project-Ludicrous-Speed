﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsView : Overlay
{
    [SerializeField] TextMeshProUGUI creditsText;
    
    public void SetCreditsText(int credits) {
        creditsText.text = $"Credits: {credits.ToString()}";
    }
}