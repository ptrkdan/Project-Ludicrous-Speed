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
        contractSelectCanvas.gameObject.SetActive(true);
    }

    public void OpenContractDetails() {
        contractSelectCanvas.gameObject.SetActive(false);
        contractDetailsCanvas.gameObject.SetActive(true);
    }

    
}
