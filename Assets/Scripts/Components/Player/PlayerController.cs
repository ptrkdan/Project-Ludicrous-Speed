using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : LivingInteractable
{
    [Header("Projectile")]
    [SerializeField] Projectile projectile;
    
    private float movementXMin;
    private float movementXMax;
    private float movementYMin;
    private float movementYMax;
    private Coroutine fireLaserCoroutine;

    Vector3 movement = new Vector2();

    GameSession session;
    Rigidbody2D rigidBody;

    void Start() {
        session = FindObjectOfType<GameSession>();
        rigidBody = GetComponent<Rigidbody2D>();

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
            fireLaserCoroutine = StartCoroutine(FireLaser());
        }
        if (Input.GetButtonUp("Fire1")) {
            StopCoroutine(fireLaserCoroutine);
        }
    }

    IEnumerator FireLaser() {
        while (true) {
            Projectile laser = Instantiate(
                projectile, 
                transform.position + projectile.Config.Offset,
                Quaternion.AngleAxis(-90, Vector3.forward));
            laser.Fire();

            yield return new WaitForSeconds(projectile.Config.ShotCooldown);
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
