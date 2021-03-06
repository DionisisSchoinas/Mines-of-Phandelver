using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneRay : SpellTypeRay
{
    public override string skillName => "Stone Ray";

    private void Start()
    {
        condition = null;
    }

    public override ParticleSystem GetSource()
    {
        return ResourceManager.Sources.Spells.Earth;
    }
}
