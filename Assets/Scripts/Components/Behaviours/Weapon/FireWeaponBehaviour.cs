public class FireWeaponBehaviour : WeaponBehaviour
{
    public override BehaviourState Do()
    {
        CountdownAndShoot();
        SetBehaviourState();

        return CurrentState;
    }
}
