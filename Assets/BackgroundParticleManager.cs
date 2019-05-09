using UnityEngine;

public class BackgroundParticleManager : MonoBehaviour
{
    [SerializeField] ParticleSystem starfieldFar;
    [SerializeField] ParticleSystem starfieldNear;
    [SerializeField] BackgroundScroller bgScroller;

    [Header("Factors")]
    [SerializeField] float starfieldFarVelocityFactor = 1f;
    [SerializeField] float starfieldNearVelocityFactor = 1f;
    [SerializeField] float backgroundScrollerVelocityFactor = 1f;

    ParticleSystem.VelocityOverLifetimeModule starfieldFarVelocity;
    ParticleSystem.VelocityOverLifetimeModule starfieldNearVelocity;
    public void Awake()
    {
        starfieldFarVelocity = starfieldFar.velocityOverLifetime;
        starfieldNearVelocity = starfieldNear.velocityOverLifetime;
    }

    public void AddVelocity(float speed)
    {
        starfieldFarVelocity.speedModifierMultiplier += speed * starfieldFarVelocityFactor;
        starfieldNearVelocity.speedModifierMultiplier += speed * starfieldNearVelocityFactor;
        bgScroller.AddVelocity(speed * backgroundScrollerVelocityFactor);
    }

    public void RemoveVelocity(float speed)
    {
        starfieldFarVelocity.speedModifierMultiplier -= speed * starfieldFarVelocityFactor;
        starfieldNearVelocity.speedModifierMultiplier -= speed * starfieldNearVelocityFactor;
        bgScroller.RemoveVelocity(speed * backgroundScrollerVelocityFactor);
    }

}
