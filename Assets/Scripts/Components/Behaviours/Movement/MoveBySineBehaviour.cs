using UnityEngine;

public class MoveBySineBehaviour : MovementBehaviour
{
    private const float MAGNITUDE_FACTOR = 0.01f;

    [SerializeField] private float magnitude = 1f;
    [SerializeField] private float frequency = 1f;
    private float randomness;

    #region Methods: Unity 

    private void Awake()
    {
        randomness = Random.Range(0, 180);
    }

    #endregion Methods: Unity 

    public override BehaviourState Do()
    {
        Move();
        SetBehaviourState();

        return CurrentState;
    }

    protected override void Move()
    {
        float engineFactor = stats.GetStat(StatType.Engine).Value;
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
