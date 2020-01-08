using UnityEngine;

public class PickUp : LootController
{
    [SerializeField, Range(0, 1f)] private float spawnRate = 0;

    public override void Interact(Interactable other)
    {
        base.Interact(other);
        Destroy(gameObject);
    }
}
