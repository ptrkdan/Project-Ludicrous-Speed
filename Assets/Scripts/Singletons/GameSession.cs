using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] ContractConfig activeContract;
    [SerializeField] bool isRunSuccessful;
    [SerializeField] Canvas quitConfirmationOverlayPrefab;

    public ContractConfig ActiveContract { get => activeContract; set => activeContract = value; }
    public bool IsRunSuccessful { get => isRunSuccessful; set => isRunSuccessful = value; }

    Canvas quitConfirmationOverlay;
    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && quitConfirmationOverlay == null)
        {
            Time.timeScale = 0;
            quitConfirmationOverlay = Instantiate(quitConfirmationOverlayPrefab);
        }
    }
}
