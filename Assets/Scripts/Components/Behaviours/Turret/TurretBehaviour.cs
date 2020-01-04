using System.Collections.Generic;
using UnityEngine;

public abstract class TurretBehaviour : Behaviour
{
    [SerializeField] protected List<Transform> turrets;

    #region Methods: Unity 

    private void Awake()
    {
        SetTurretAngle();
    }

    #endregion Methods: Unity 

    protected abstract void SetTurretAngle();

    #region Methods: Behaviour State

    protected override void SetBehaviourState()
    {
        // No particular state change
    }

    #endregion Methods: Behaviour State

}
