using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceray : SpellTypeRay
{
    public override string skillName => "Ice Ray";

    private void Start()
    {
        condition = ConditionsManager.Frozen;
    }

    public override ParticleSystem GetSource()
    {
        return ResourceManager.Sources.Spells.Ice;
    }
}
