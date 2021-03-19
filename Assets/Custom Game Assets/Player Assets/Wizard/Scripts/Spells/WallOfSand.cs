using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfSand : SpellTypeWall
{
    public override string skillName => "Sand Wall";

    private void Start()
    {
    }

    public override ParticleSystem GetSource()
    {
        return ResourceManager.Sources.Spells.Earth;
    }
}
