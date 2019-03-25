using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] PlayerSingleton player;
    [SerializeField] ContractConfig activeContract;
    [SerializeField] bool isRunSuccessful;

    public PlayerSingleton Player { get => player; set => player = value; }
    public ContractConfig ActiveContract { get => activeContract; set => activeContract = value; }
    public bool IsRunSuccessful { get => isRunSuccessful; set => isRunSuccessful = value; }
}
