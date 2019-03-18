using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserSlow : Laser
{
    public override void Fire() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-config.Speed, 0);
        AudioSource.PlayClipAtPoint(config.ShootSFX, Camera.main.transform.position, config.ShootSFXVolume);
    }
}
