using UnityEngine;

public class FireConeWave : ConeBurstSlash
{
    public override string skillName => "Fire Cone Wave";

    void Start()
    {
        condition = ConditionsManager.Burning;
    }

    public override GameObject GetSource()
    {
        return ResourceManager.Sources.SwordEffects.Fire;
    }
}
