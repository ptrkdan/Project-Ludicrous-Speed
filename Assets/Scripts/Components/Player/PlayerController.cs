using System.Collections;
using UnityEngine;

public class PlayerController : LivingInteractable
{
    private const float START_POS_X = 10f;
    private const float START_POS_Y = 8f;

    [SerializeField] private Transform turret;

    [Header("Equipments")]
    [SerializeField] private Weapon primaryWpn;
    [SerializeField] private Weapon secondaryWpn;
    [SerializeField] private SupportEquipment supportEquip;

    [Space]
    [SerializeField] private float engineValueFactor = 0.5f;
    private float movementXMin;
    private float movementXMax;
    private float movementYMin;
    private float movementYMax;
    private Vector3 movement = new Vector2();
    private PlayerSingleton player;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private bool isFiringPrimaryWpn = false;
    private bool isFiringSecondaryWpn = false;
    private Coroutine primaryWpnCoroutine;
    private Coroutine secondaryWpnCoroutine;
    private bool isControllable = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerSingleton>();
        rigidBody = GetComponent<Rigidbody2D>();

        SetEquipment();
        SetMovementBoundaries();
    }

    private void Update()
    {
        if (isControllable)
        {
            CheckInput();
        }
    }

    private void FixedUpdate()
    {
        if (isControllable)
        {
            Move();
        }
    }

    public void EnableControls()
    {
        isControllable = true;
    }

    public void DisableControls()
    {
        isControllable = false;

        // Stop any weapon coroutines
        StopAllCoroutines();
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

        primaryWpn.SetTurretPosition(turret);
        secondaryWpn.SetTurretPosition(turret);
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
            primaryWpnCoroutine = StartCoroutine(ActivatePrimaryWeapon());
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(primaryWpnCoroutine);
            DeactivatePrimaryWeapon();
        }
    }

    private void CheckFireSecondaryWeaponInput()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isFiringSecondaryWpn = true;
            secondaryWpnCoroutine = StartCoroutine(ActivateSecondaryWeapon());
        }
        if (Input.GetButtonUp("Fire2"))
        {
            StopCoroutine(secondaryWpnCoroutine);
            DeactivateSecondaryWeapon();
        }
    }

    private IEnumerator ActivatePrimaryWeapon()
    {
        while (true)
        {
            primaryWpn.Activate();

            if (primaryWpn.GetWeaponType() == WeaponType.Auto)
            {
                yield return new WaitForSeconds(primaryWpn.GetShotCooldown().Value);
            }
            else if (primaryWpn.GetWeaponType() == WeaponType.Charged)
            {
                yield return 0;
            }

            yield return 0;
        }
    }

    private void DeactivatePrimaryWeapon()
    {
        primaryWpn.Deactivate();
    }

    private IEnumerator ActivateSecondaryWeapon()
    {
        while (true)
        {
            secondaryWpn.Activate();

            if (secondaryWpn.GetWeaponType() == WeaponType.Auto)
            {
                yield return new WaitForSeconds(secondaryWpn.GetShotCooldown().Value);
            }
            else if (secondaryWpn.GetWeaponType() == WeaponType.Charged)
            {
                yield return 0;
            }

            yield return 0;
        }
    }

    private void DeactivateSecondaryWeapon()
    {
        secondaryWpn.Deactivate();
    }

    private void Move()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        float playerEngineValue =
            StatsManager.instance.GetStat(StatType.Engine).Value * engineValueFactor;
        Vector3 newPosition =
            transform.position + movement * playerEngineValue * Time.fixedDeltaTime;
        newPosition.Set(
            Mathf.Clamp(newPosition.x, movementXMin, movementXMax),
            Mathf.Clamp(newPosition.y, movementYMin, movementYMax),
            0);
        rigidBody.MovePosition(newPosition);
    }

}
