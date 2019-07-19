using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InteractableStats))]
public abstract class MovementBehaviour : Behaviour
{
    protected Rigidbody2D rigidBody;
    protected InteractableStats stats;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        stats = GetComponent<InteractableStats>();
    }

    protected abstract void Move();
}
