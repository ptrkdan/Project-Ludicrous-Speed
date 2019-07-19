using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehaviour : Behaviour
{
    protected EnemyWeapon weapon;

    public EnemyWeapon GetWeapon() => weapon;
    public virtual void SetWeapon(EnemyWeapon weapon) => this.weapon = weapon;
}
