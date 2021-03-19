using UnityEngine;

public class EarthSimpleSwing : ElementalSlash
{
    public override string skillName => "Earth Slash";

    void Start()
    {
        condition = null;
    }

    public override GameObject GetSource()
    {
        return ResourceManager.Sources.SwordEffects.Earth;
    }
}
