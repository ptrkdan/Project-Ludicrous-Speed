using System.Collections.Generic;
using UnityEngine;

public class MenuLoader : MonoBehaviour {
    [SerializeField] GameSession session;
    [SerializeField] SceneLoader sceneLoader;

    [SerializeField] Canvas worldCanvas;
    [SerializeField] Canvas contractSelectCanvas;
    [SerializeField] Canvas contractDetailsCanvas;
    [SerializeField] Canvas apartmentCanvas;

    Stack<Canvas> overlayStack;     // Current overlay will always be on the top of the stack

    private void Start() {
        session = FindObjectOfType<GameSession>();
        if(!session) {
            sceneLoader.GoToPreload();
        }

        overlayStack = new Stack<Canvas>();
        ResetHQScene();
    }

    private void HideCurrentOverlay() {
        overlayStack.Peek().gameObject.SetActive(false);
    }

    private void ShowCurrentOverlay() {
        overlayStack.Peek().gameObject.SetActive(true);
    }

    private void ResetHQScene() { 
        // TODO: Deactive overlays from stack instead

        worldCanvas.gameObject.SetActive(true);
        contractSelectCanvas.gameObject.SetActive(false);
        contractDetailsCanvas.gameObject.SetActive(false);

        overlayStack.Push(worldCanvas);
    }

    public void GoToNextOverlay(Canvas nextCanvas) {
        HideCurrentOverlay();
        overlayStack.Push(nextCanvas);
        ShowCurrentOverlay();
    }

    public void OpenContractSelect() {
        GoToNextOverlay(contractSelectCanvas);
    }

    public void OpenContractDetails() {
        GoToNextOverlay(contractDetailsCanvas);
    }

    public void GoBack() {
        if (overlayStack.Count > 1) {
            HideCurrentOverlay();
            overlayStack.Pop();
            ShowCurrentOverlay();
        } else {
            ResetHQScene();
        }
    }
}
