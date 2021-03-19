using UnityEngine;

public class Firerain : SpellTypeStorm
{
    public override string skillName => "Fire Storm";

    private void Start()
    {
        condition = ConditionsManager.Burning;
    }

    public override ParticleSystem GetSource()
    {
        return ResourceManager.Sources.Spells.Fire;
    }
}