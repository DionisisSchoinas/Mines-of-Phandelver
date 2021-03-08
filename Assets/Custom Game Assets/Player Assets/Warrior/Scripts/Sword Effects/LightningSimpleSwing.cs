using UnityEngine;

public class LightningSimpleSwing : ElementalSlash
{
    public override string skillName => "Lightning Slash";

    void Start()
    {
        damageType = DamageTypesManager.Lightning;
        condition = ConditionsManager.Electrified;
    }
    public override GameObject GetSource()
    {
        return ResourceManager.Sources.SwordEffects.Lightning;
    }
}
