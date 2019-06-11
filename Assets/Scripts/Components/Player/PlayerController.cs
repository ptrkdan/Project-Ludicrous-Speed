using System.Collections;
using UnityEngine;

public class PlayerController : LivingInteractable
{
    [SerializeField] Transform weaponPosition;

    [Header("Equipments")]
    [SerializeField] Weapon primaryWpn;
    [SerializeField] Weapon secondaryWpn;
    [SerializeField] SupportEquipment supportEquip;

    [Space]
    [SerializeField] float engineValueFactor = 0.5f;
    private float movementXMin;
    private float movementXMax;
    private float movementYMin;
    private float movementYMax;

    Vector3 movement = new Vector2();

    PlayerSingleton player;
    Rigidbody2D rigidBody;
    bool isFiringPrimaryWpn = false;
    bool isFiringSecondaryWpn = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerSingleton>();
        rigidBody = GetComponent<Rigidbody2D>();

        SetEquipment();
        SetMovementBoundaries();
    }

    private void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Interact(Interactable other)
    {
        GetComponent<DamageDealer>().DealDamage(other);
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

    private void CheckInput()
    {
        CheckFirePrimaryWeaponInput();
        CheckFireSecondaryWeaponInput();
    }

    private void CheckFirePrimaryWeaponInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isFiringPrimaryWpn = true;
            StartCoroutine(UsePrimaryWeapon());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            isFiringPrimaryWpn = false;
        }
    }

    private void CheckFireSecondaryWeaponInput()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isFiringSecondaryWpn = true;
            Debug.LogAssertion($"isFiringSecondaryWpn: {isFiringSecondaryWpn}");
            StartCoroutine(UseSecondaryWeapon());
        }
        if (Input.GetButtonUp("Fire2"))
        {
            isFiringSecondaryWpn = false;
        }
    }

    IEnumerator UsePrimaryWeapon()
    {
        while (isFiringPrimaryWpn)
        {
            primaryWpn.Activate(weaponPosition.position, Quaternion.AngleAxis(-90, Vector3.forward));

            yield return new WaitForSeconds(primaryWpn.GetCooldown().GetCalcValue());
        }
    }

    IEnumerator UseSecondaryWeapon()
    {
        while (isFiringPrimaryWpn)
        {
            secondaryWpn.Activate(weaponPosition.position, Quaternion.AngleAxis(-90, Vector3.forward));

            yield return new WaitForSeconds(secondaryWpn.GetCooldown().GetCalcValue());
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
