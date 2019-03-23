using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RunResultsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultsText;
    [SerializeField] Transform lootPanel;

    [SerializeField] GameSession session;
    // Start is called before the first frame update
    void Start()
    {
        session = FindObjectOfType<GameSession>();

        SetResultsText();
        
    }

    private void SetResultsText() {
        if(session.IsRunSuccessful) {
            resultsText.text = "Successful!";
        } else {
            resultsText.text = "Busted!";
        }
    }
}
