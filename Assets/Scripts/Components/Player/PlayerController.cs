using System.Collections;
using UnityEngine;

public class PlayerController : LivingInteractable
{

    [Header("Equipments")]
    [SerializeField] Weapon primaryWpn;
    [SerializeField] Weapon secondaryWpn;
    [SerializeField] SupportEquipment supportEquip;
    [SerializeField] Projectile projectile;

    [Space]
    [SerializeField] float engineValueFactor = 0.5f;
    private float movementXMin;
    private float movementXMax;
    private float movementYMin;
    private float movementYMax;
    private Coroutine primaryWeaponCoroutine;

    Vector3 movement = new Vector2();

    PlayerSingleton player;
    Rigidbody2D rigidBody;

    private void Start()
    {
        player = FindObjectOfType<PlayerSingleton>();
        rigidBody = GetComponent<Rigidbody2D>();

        SetEquipment();
        SetMovementBoundaries();
    }

    private void Update()
    {
        Fire();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Interact(Interactable other)
    {
        GetComponent<DamageDealer>().Interact(other);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    private void SetEquipment()
    {
        primaryWpn = (Weapon)player.GetEquipment(EquipmentSlot.PrimaryWeapon);
        secondaryWpn = (Weapon)player.GetEquipment(EquipmentSlot.SecondaryWeapon);
        supportEquip = (SupportEquipment)player.GetEquipment(EquipmentSlot.Support);
    }


    private void SetMovementBoundaries()
    {
        Camera gameCamera = Camera.main;
        RectTransform hudPanel = FindObjectOfType<HudPanel>().GetComponent<RectTransform>();
        movementXMin = gameCamera.ViewportToWorldPoint(Vector3.zero).x;
        movementXMax = gameCamera.ViewportToWorldPoint(Vector3.right).x;
        movementYMin = gameCamera.ViewportToWorldPoint(Vector3.zero).y;
        movementYMax = gameCamera.ViewportToWorldPoint(Vector3.up).y - hudPanel.localScale.y;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            primaryWeaponCoroutine = StartCoroutine(UsePrimaryWeapon());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(primaryWeaponCoroutine);
        }
    }

    IEnumerator UsePrimaryWeapon()
    {
        while (true)
        {
            primaryWpn.Interact(transform.position, Quaternion.AngleAxis(-90, Vector3.forward));

            yield return new WaitForSeconds(primaryWpn.GetCooldown().GetCalcValue());
        }
    }

    private void Move()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        float playerEngineValue =
            StatsManager.instance.GetStat(StatType.Engine).GetCalcValue() * engineValueFactor;
        Vector3 newPosition =
            transform.position + movement * playerEngineValue * Time.fixedDeltaTime;
        newPosition.Set(
            Mathf.Clamp(newPosition.x, movementXMin, movementXMax),
            Mathf.Clamp(newPosition.y, movementYMin, movementYMax),
            0);
        rigidBody.MovePosition(newPosition);
    }
}
