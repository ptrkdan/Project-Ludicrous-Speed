using UnityEngine;

public class BackgroundParticleManager : MonoBehaviour
{
    [SerializeField] ParticleSystem starfieldFar;
    [SerializeField] ParticleSystem starfieldNear;
    [SerializeField] BackgroundScroller bgScroller;

    [Space]
    [SerializeField] float masterSpeedFactor = 1f;

    [Header("Starfield Far Factors")]
    [SerializeField] float starfieldFarEmissionFactor = 1f;
    [SerializeField] float starfieldFarVelocityFactor = 1f;

    [Header("Starfield Near Factors")]
    [SerializeField] float starfieldNearEmissionFactor = 1f;
    [SerializeField] float starfieldNearVelocityFactor = 1f;

    [Header("BG Scroller Factor")]
    [SerializeField] float backgroundScrollerVelocityFactor = 1f;

    ParticleSystem.MainModule starfieldFarMain;
    ParticleSystem.MainModule starfieldNearMain;
    ParticleSystem.EmissionModule starfieldFarEmission;
    ParticleSystem.EmissionModule starfieldNearEmission;
    ParticleSystem.VelocityOverLifetimeModule starfieldFarVelocity;
    ParticleSystem.VelocityOverLifetimeModule starfieldNearVelocity;

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
            speed * masterSpeedFactor* starfieldNearVelocityFactor;

        bgScroller.UpdateVelocity(speed * masterSpeedFactor * backgroundScrollerVelocityFactor);
    }
}
