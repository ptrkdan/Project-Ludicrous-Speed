using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] ContractConfig activeContract;
    [SerializeField] bool isRunSuccessful;

    public ContractConfig ActiveContract { get => activeContract; set => activeContract = value; }
    public bool IsRunSuccessful { get => isRunSuccessful; set => isRunSuccessful = value; }

    private void Awake() {
        SetUpSingleton();
    }

    private void SetUpSingleton() {
        if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }
}
