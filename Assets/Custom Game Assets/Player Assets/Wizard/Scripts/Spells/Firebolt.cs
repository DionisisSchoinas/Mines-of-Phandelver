using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebolt : SpellTypeBolt
{
    public override string skillName => "Fire Bolt";

    private void Start()
    {
        condition = ConditionsManager.Burning;
    }

    public override ParticleSystem GetSource()
    {
        return ResourceManager.Sources.Spells.Fire;
    }
}
