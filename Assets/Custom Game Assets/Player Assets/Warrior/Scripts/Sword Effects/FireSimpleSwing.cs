using UnityEngine;

public class FireSimpleSwing : ElementalSlash
{
    public override string skillName => "Fire Slash";

    void Start()
    {
        condition = ConditionsManager.Burning;
    }

    public override GameObject GetSource()
    {
        return ResourceManager.Sources.SwordEffects.Fire;
    }
}
