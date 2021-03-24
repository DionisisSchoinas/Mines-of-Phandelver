using System.Collections.Generic;
using UnityEngine;

public class LightningStorm : SpellTypeStorm
{
    public override string skillName => "Lightning Storm";

    private void Start()
    {
        condition = ConditionsManager.Electrified;
    }

    public override ParticleSystem GetSource()
    {
        return ResourceManager.Sources.Spells.Lightning;
    }
}