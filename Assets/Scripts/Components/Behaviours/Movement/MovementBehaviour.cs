using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InteractableStats))]
public abstract class MovementBehaviour : Behaviour
{
    protected Rigidbody2D rigidBody;
    protected InteractableStats stats;

    #region Methods: Unity 

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        stats = GetComponent<InteractableStats>();
    }

    #endregion Methods: Unity 

    protected abstract void Move();

    #region Methods: Behaviour State

    protected override void SetBehaviourState()
    {
        CurrentState |= BehaviourState.Moved;
    }

    #endregion Methods: Behaviour State
}
