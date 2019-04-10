using UnityEngine;

public abstract class Laser : Projectile
{
    public override Projectile Fire() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);

        return this;
    }

    public override Projectile WithDamage(int value) {
        GetComponent<DamageDealer>().Damage = value;
        return this;
    }

    public override Projectile WithSpeed(float value) {
        speed = value;
        return this;
    }
}
