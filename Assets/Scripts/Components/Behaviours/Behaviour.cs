using UnityEngine;

public abstract class Behaviour : MonoBehaviour
{
    public abstract BehaviourState Do(BehaviourState currentState);

}
[System.Flags]
public enum BehaviourState {
    None        = 0,
    Moved       = 1,
    Fired       = 2,
    Interacted  = 4
}
