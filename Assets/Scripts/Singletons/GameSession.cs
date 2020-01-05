using UnityEngine;

public class GameSession : MonoBehaviour
{
    #region Singleton
    public static GameSession instance;
    private void Awake()
    {
        if (instance)
        {
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] private ContractConfig activeContract;
    [SerializeField] private bool isRunSuccessful;
    [SerializeField] private Canvas quitConfirmationOverlayPrefab;

    public ContractConfig ActiveContract { get => activeContract; set => activeContract = value; }
    public bool IsRunSuccessful { get => isRunSuccessful; set => isRunSuccessful = value; }

    public delegate void OnMissionComplete(bool success, ContractConfig contract);
    public OnMissionComplete OnMissionCompleteCallback;

    public delegate void OnCampaignMissionComplete(bool success, int campaignID);
    public OnCampaignMissionComplete OnCampaignMissionCompleteCallback;
    private Canvas quitConfirmationOverlay;
    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && quitConfirmationOverlay == null)
        {
            Time.timeScale = 0;
            quitConfirmationOverlay = Instantiate(quitConfirmationOverlayPrefab);
        }
    }
}
