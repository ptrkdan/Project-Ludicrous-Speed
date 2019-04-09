using System;
using UnityEngine;

[RequireComponent(typeof(LootController))]
public class PickUp : Interactable
{
    [SerializeField] [Range(0,1f)] float spawnRate = 0;

    public float SpawnRate { get => spawnRate; set => spawnRate = value; }

    public override void Interact(Interactable other) {
        base.Interact(other);
        Destroy(gameObject);
    }
}
