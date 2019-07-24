using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBySineBehaviour : MovementBehaviour
{
    const float MAGNITUDE_FACTOR = 0.01f;

    [SerializeField] float magnitude = 1f;
    [SerializeField] float frequency = 1f;

    float randomness;

    private void Awake()
    {
        randomness = Random.Range(0, 180);
    }

    public override BehaviourState Do(BehaviourState currentState)
    {
        Move();

        return currentState | BehaviourState.Moved;
    }

    protected override void Move()
    {
        float engineFactor = stats.GetStat(StatType.Engine).GetCalcValue();
        Vector3 xMovement = Vector3.right
            * transform.parent.localScale.x
            * engineFactor
            * Time.deltaTime;

        float sineFactor = Mathf.Sin(Time.time * frequency + randomness) * magnitude * MAGNITUDE_FACTOR;
        Vector3 yMovement = Vector3.up
            * sineFactor;
        Vector3 movement = xMovement + yMovement;
        rigidBody.MovePosition(transform.position + movement);
    }
}
