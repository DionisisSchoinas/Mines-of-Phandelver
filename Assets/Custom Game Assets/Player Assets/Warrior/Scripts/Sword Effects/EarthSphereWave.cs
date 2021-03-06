using UnityEngine;

public class EarthSphereWave : SphereBurst
{
    public override string skillName => "Earth Sphere Wave";

    void Start()
    {
        condition = null;
    }

    public override GameObject GetSource()
    {
        return ResourceManager.Sources.SwordEffects.Earth;
    }
}
