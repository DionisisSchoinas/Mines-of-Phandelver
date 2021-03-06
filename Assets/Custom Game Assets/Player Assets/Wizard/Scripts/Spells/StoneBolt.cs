using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBolt : SpellTypeBolt
{
    public override string skillName => "Stone Bolt";

    private void Start()
    {
        condition = null;
    }

    public override ParticleSystem GetSource()
    {
        return ResourceManager.Sources.Spells.Earth;
    }
}
