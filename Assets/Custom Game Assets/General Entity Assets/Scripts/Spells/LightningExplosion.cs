
public class LightningExplosion : Explosion
{
    private new void Start()
    {
        condition = ConditionsManager.Electrified;
        base.Start();
    }
}