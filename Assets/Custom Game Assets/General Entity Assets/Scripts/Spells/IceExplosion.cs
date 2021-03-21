
public class IceExplosion : Explosion
{
    private new void Start()
    {
        condition = ConditionsManager.Frozen;
        base.Start();
    }
}
