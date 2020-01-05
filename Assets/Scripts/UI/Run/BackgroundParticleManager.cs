using UnityEngine;

public class BackgroundParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem starfieldFar;
    [SerializeField] private ParticleSystem starfieldNear;
    [SerializeField] private BackgroundScroller bgScroller;

    [Space]
    [SerializeField] private float masterSpeedFactor = 1f;

    [Header("Starfield Far Factors")]
    [SerializeField] private float starfieldFarEmissionFactor = 1f;
    [SerializeField] private float starfieldFarVelocityFactor = 1f;

    [Header("Starfield Near Factors")]
    [SerializeField] private float starfieldNearEmissionFactor = 1f;
    [SerializeField] private float starfieldNearVelocityFactor = 1f;

    [Header("BG Scroller Factor")]
    [SerializeField] private float backgroundScrollerVelocityFactor = 1f;

    private ParticleSystem.MainModule starfieldFarMain;
    private ParticleSystem.MainModule starfieldNearMain;
    private ParticleSystem.EmissionModule starfieldFarEmission;
    private ParticleSystem.EmissionModule starfieldNearEmission;
    private ParticleSystem.VelocityOverLifetimeModule starfieldFarVelocity;
    private ParticleSystem.VelocityOverLifetimeModule starfieldNearVelocity;

    public void Awake()
    {
        starfieldFarMain = starfieldFar.main;
        starfieldFarEmission = starfieldFar.emission;
        starfieldFarVelocity = starfieldFar.velocityOverLifetime;

        starfieldNearMain = starfieldNear.main;
        starfieldNearEmission = starfieldNear.emission;
        starfieldNearVelocity = starfieldNear.velocityOverLifetime;
    }

    public void UpdateVelocity(float speed)
    {
        Debug.Log($"Modifying velocity by {speed}");


        // FIXME : Still not being modified as intended
        starfieldFarMain.startLifetimeMultiplier =
            speed * masterSpeedFactor * starfieldFarVelocityFactor;
        starfieldFarEmission.rateOverTime =
            speed * masterSpeedFactor * starfieldFarEmissionFactor;
        starfieldFarVelocity.speedModifierMultiplier =
            speed * masterSpeedFactor * starfieldFarVelocityFactor;

        starfieldNearMain.startLifetimeMultiplier =
            speed * masterSpeedFactor * starfieldNearVelocityFactor;
        starfieldNearEmission.rateOverTimeMultiplier =
            speed * masterSpeedFactor * starfieldNearEmissionFactor;
        starfieldNearVelocity.speedModifierMultiplier =
            speed * masterSpeedFactor * starfieldNearVelocityFactor;

        bgScroller.UpdateVelocity(speed * masterSpeedFactor * backgroundScrollerVelocityFactor);
    }
}
