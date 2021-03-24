using UnityEngine;

public class Firewall : SpellTypeWall
{
    public override string skillName => "Fire Wall";

    private void Start()
    {
        condition = ConditionsManager.Burning;
    }

    public override ParticleSystem GetSource()
    {
        return ResourceManager.Sources.Spells.Fire;
    }
}