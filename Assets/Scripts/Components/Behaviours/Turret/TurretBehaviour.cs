using System.Collections.Generic;
using UnityEngine;

public abstract class TurretBehaviour : Behaviour
{
    [SerializeField] protected List<Transform> turrets;

    private void Awake()
    {
        SetTurretAngle();
    }

    protected abstract void SetTurretAngle();

}
