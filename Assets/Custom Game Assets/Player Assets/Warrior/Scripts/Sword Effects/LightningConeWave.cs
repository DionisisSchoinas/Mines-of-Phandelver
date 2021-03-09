using UnityEngine;

public class LightningConeWave : ConeBurstSlash
{
    public override string skillName => "Lightning Cone Wave";

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
