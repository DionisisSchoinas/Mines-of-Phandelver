
public class FireExplosion : Explosion
{
    private new void Start()
    {
        condition = ConditionsManager.Burning;
        base.Start();
    }
}
