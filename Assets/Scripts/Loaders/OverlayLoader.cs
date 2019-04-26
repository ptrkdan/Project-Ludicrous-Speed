using System.Collections.Generic;
using UnityEngine;

public class OverlayLoader : MonoBehaviour {

    [SerializeField] Canvas worldCanvas;
    [SerializeField] ContractSelectOverlay contractSelectOverlayPrefab;
    [SerializeField] ContractDetailsOverlay contractDetailsOverlayPrefab;
    [SerializeField] ApartmentOverlay apartmentOverlayPrefab;
    [SerializeField] HangarOverlay hangarShipOverlayPrefab;
    [SerializeField] MarketOverlay marketOverlayPrefab;
    [SerializeField] OfficeOverlay officeOverlayPrefab;

    GameSession session;
    SceneLoader sceneLoader;
    Stack<Overlay> overlayStack;     // Current overlay will always be on the top of the stack
    

    private void Awake() {
        session = FindObjectOfType<GameSession>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        if(!session) {
            sceneLoader.GoToPreload();
        }

        overlayStack = new Stack<Overlay>();
        ResetHQScene();
    }

    private void HideCurrentOverlay() {
        if (overlayStack.Count > 1) {
            overlayStack.Peek().gameObject.SetActive(false);
        }
    }

    private void ShowCurrentOverlay() {
        if (overlayStack.Count > 1) {
            overlayStack.Peek().gameObject.SetActive(true);
        }
    }

    private void ResetHQScene() {
        // TODO: Deactive overlays from stack instead

        if (overlayStack.Count > 1) {
            Overlay overlay = overlayStack.Pop();
            while (overlay != null) {
                Destroy(overlay);
                overlay = overlayStack.Pop();
            }        
        }
    }

    private void GoToNextOverlay(Overlay nextOverlayPrefab) {
        HideCurrentOverlay();
        Overlay nextOverlay = Instantiate(nextOverlayPrefab);
        overlayStack.Push(nextOverlay);
        nextOverlay.Display();
    }

    public void OpenContractSelectOverlay() {
        GoToNextOverlay(contractSelectOverlayPrefab);
    }

    public void OpenContractDetailsOverlay() {
        GoToNextOverlay(contractDetailsOverlayPrefab);
    }

    public void OpenApartmentOverlay() {
        GoToNextOverlay(apartmentOverlayPrefab);
    }

    public void OpenHangarOverlay() {
        GoToNextOverlay(hangarShipOverlayPrefab);
    }
    
    public void OpenMarketOverlay() {
        GoToNextOverlay(marketOverlayPrefab);
    }

    public void OpenOfficeOverlay() {
        GoToNextOverlay(officeOverlayPrefab);
    }

    public void GoBack(Overlay currentOverlay) {
        Destroy(currentOverlay.gameObject);
        overlayStack.Pop();
        ShowCurrentOverlay();
    }
}
