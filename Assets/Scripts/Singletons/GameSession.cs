using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] ContractConfig activeContract;
    [SerializeField] bool isRunSuccessful;

    public ContractConfig ActiveContract { get => activeContract; set => activeContract = value; }
    public bool IsRunSuccessful { get => isRunSuccessful; set => isRunSuccessful = value; }
}
