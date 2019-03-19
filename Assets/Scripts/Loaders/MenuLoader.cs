using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLoader : MonoBehaviour {
    [SerializeField] Canvas worldCanvas;
    [SerializeField] Canvas contractSelectCanvas;
    [SerializeField] Canvas contractDetailsCanvas;

    private void Start() {
        ResetHQScene();
    }

    private void ResetHQScene() {
        worldCanvas.gameObject.SetActive(true);
        contractSelectCanvas.gameObject.SetActive(false);
        contractDetailsCanvas.gameObject.SetActive(false);
    }

    public void OpenContractSelect() {
        // Select randomized contracts from list and place on display

        worldCanvas.gameObject.SetActive(false);
        contractSelectCanvas.gameObject.SetActive(true);
        
    }

    public void OpenContractDetails(ContractConfig contract = null) {
        // Set contract details on UI

        contractSelectCanvas.gameObject.SetActive(false);
        contractDetailsCanvas.gameObject.SetActive(true);
    }

    
}
