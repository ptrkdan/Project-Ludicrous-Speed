using UnityEngine;

public class StraightPathEnemyController : EnemyController
{
    [SerializeField] float moveSpeed = 15f;

    protected override void Move() {
        rigidBody.MovePosition(transform.position + Vector3.right * moveSpeed * Time.fixedDeltaTime);
    }
}
