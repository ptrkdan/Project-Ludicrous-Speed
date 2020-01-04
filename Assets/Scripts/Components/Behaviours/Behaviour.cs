using UnityEngine;

public abstract class Behaviour : MonoBehaviour
{
    protected BehaviourState CurrentState { get; set; } = BehaviourState.None;

    public abstract BehaviourState Do();
    protected abstract void SetBehaviourState();
}
