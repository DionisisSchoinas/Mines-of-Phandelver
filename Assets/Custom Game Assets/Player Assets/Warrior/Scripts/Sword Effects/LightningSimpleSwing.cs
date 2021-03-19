using UnityEngine;

public class LightningSimpleSwing : ElementalSlash
{
    public override string skillName => "Lightning Slash";

    void Start()
    {
        condition = ConditionsManager.Electrified;
    }
    public override GameObject GetSource()
    {
        return ResourceManager.Sources.SwordEffects.Lightning;
    }
}
