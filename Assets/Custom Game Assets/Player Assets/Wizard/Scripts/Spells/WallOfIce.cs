using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfIce : SpellTypeWall
{
    public override string skillName => "Ice Wall";

    private void Start()
    {
    }

    public override ParticleSystem GetSource()
    {
        return ResourceManager.Sources.Spells.Ice;
    }
}
