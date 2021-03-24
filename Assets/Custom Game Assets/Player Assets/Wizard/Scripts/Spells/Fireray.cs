using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireray : SpellTypeRay
{
    public override string skillName => "Fire Ray";

    private void Start()
    {
        condition = ConditionsManager.Burning;
    }

    public override ParticleSystem GetSource()
    {
        return ResourceManager.Sources.Spells.Fire;
    }
}