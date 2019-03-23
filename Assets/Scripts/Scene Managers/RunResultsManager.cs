using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class RunResultsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultsText;
    [SerializeField] Transform lootPanel;
    [SerializeField] Button restartButton;
    [SerializeField] Button hqButton;

    [SerializeField] GameSession session;
    // Start is called before the first frame update
    void Start()
    {
        session = FindObjectOfType<GameSession>();

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
        // session.ActiveContract.GetContractRewards();
    }
}
