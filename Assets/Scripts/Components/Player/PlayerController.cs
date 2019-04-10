using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : LivingInteractable
{
    [Header("Equipments")]
    [SerializeField] WeaponConfig primaryWpn;
    [SerializeField] WeaponConfig secondaryWpn;
    [SerializeField] SupportEquipConfig supportEquip;
    [SerializeField] Projectile projectile;
    
    private float movementXMin;
    private float movementXMax;
    private float movementYMin;
    private float movementYMax;
    private Coroutine primaryWeaponCoroutine;

    Vector3 movement = new Vector2();

    GameSession session;
    PlayerSingleton player;
    Rigidbody2D rigidBody;

    void Start() {
        session = FindObjectOfType<GameSession>();
        player = session.Player;
        rigidBody = GetComponent<Rigidbody2D>();

        SetEquipment();
        SetMovementBoundaries();
    }

    private void SetEquipment() {
        primaryWpn = (WeaponConfig) player.GetEquipment(EquipmentSlot.PrimaryWeapon);
        secondaryWpn = (WeaponConfig) player.GetEquipment(EquipmentSlot.SecondaryWeapon);
        supportEquip = (SupportEquipConfig) player.GetEquipment(EquipmentSlot.Support);
    }

    private void SetMovementBoundaries() {
        Camera gameCamera = Camera.main;
        RectTransform hudPanel = FindObjectOfType<HudPanel>().GetComponent<RectTransform>();
        movementXMin = gameCamera.ViewportToWorldPoint(Vector3.zero).x;
        movementXMax = gameCamera.ViewportToWorldPoint(Vector3.right).x;
        movementYMin = gameCamera.ViewportToWorldPoint(Vector3.zero).y;
        movementYMax = gameCamera.ViewportToWorldPoint(Vector3.up).y - hudPanel.localScale.y;
    }

    void Update() {
        Fire();
    }

    private void FixedUpdate() {
        Move();
    }
    
    public override void Interact(Interactable other) {
        GetComponent<DamageDealer>().Interact(other);
    }

    private void Fire() {
        if (Input.GetButtonDown("Fire1")) {
            primaryWeaponCoroutine = StartCoroutine(FirePrimaryWeapon());
        }
        if (Input.GetButtonUp("Fire1")) {
            StopCoroutine(primaryWeaponCoroutine);
        }
    }

    IEnumerator FirePrimaryWeapon() {
        while (true) {
            primaryWpn.Fire(transform.position, Quaternion.AngleAxis(-90, Vector3.forward));

            yield return new WaitForSeconds(primaryWpn.Cooldown.GetCalcValue());
        }
    }

    private void Move() {   
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 newPosition = transform.position + movement * stats.speed.GetCalcValue() * Time.fixedDeltaTime;
        newPosition.Set(
            Mathf.Clamp(newPosition.x, movementXMin, movementXMax), 
            Mathf.Clamp(newPosition.y, movementYMin, movementYMax),
            0); 
        rigidBody.MovePosition(newPosition);
    }    
}
