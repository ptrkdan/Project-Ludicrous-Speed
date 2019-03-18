using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLoader : MonoBehaviour {
    [SerializeField] Canvas worldCanvas;
    [SerializeField] Canvas contractSelectCanvas;

    public void OpenContractSelect() {
        contractSelectCanvas.gameObject.SetActive(true);
    }

    public void StartContract() {
    }

    
}
