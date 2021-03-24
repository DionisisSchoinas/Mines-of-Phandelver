using UnityEngine;

public class EarthConeWave : ConeBurstSlash
{
    public override string skillName => "Earth Cone Wave";

    void Start()
    {
        condition = null;
    }

    public override GameObject GetSource()
    {
        return ResourceManager.Sources.SwordEffects.Earth;
    }
}
