using UnityEngine;

public class EnemyLaserSlow : Laser
{
    public override void Fire() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-config.Speed, 0);
        PlayFireSFX();
    }
}
