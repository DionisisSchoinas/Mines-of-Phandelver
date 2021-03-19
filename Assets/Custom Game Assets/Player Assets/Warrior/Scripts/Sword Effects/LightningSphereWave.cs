using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSphereWave : SphereBurst
{
    public override string skillName => "Lightning Sphere Wave";

    void Start()
    {
        condition = ConditionsManager.Electrified;
    }

    public override GameObject GetSource()
    {
        return ResourceManager.Sources.SwordEffects.Lightning;
    }
}
