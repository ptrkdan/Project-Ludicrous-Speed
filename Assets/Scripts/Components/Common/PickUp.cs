using System;
using UnityEngine;

public class PickUp : Interactable
{
    [SerializeField] [Range(0,1f)] float spawnRate = 0;

    public float SpawnRate { get => spawnRate; set => spawnRate = value; }
}
