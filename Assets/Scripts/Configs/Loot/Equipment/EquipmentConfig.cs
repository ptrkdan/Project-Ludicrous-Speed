﻿using UnityEngine;

public class EquipmentConfig : LootConfig
{
    [SerializeField] EquipmentSlot equipSlot;

    public EquipmentSlot EquipSlot { get => equipSlot; }

    public override void Use() {
        base.Use();
        EquipmentManager.instance.EquipItem(this);
    }

    public virtual void Fire(Vector3 projectileOrigin, Quaternion rotation) {

    }
}

public enum EquipmentSlot { PrimaryWeapon, SecondaryWeapon, Support }
