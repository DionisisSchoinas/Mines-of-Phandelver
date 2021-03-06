using UnityEngine;

public class IceResistance : ResistanceEffect
{
    public override string skillName => "Ice Resistance";

    private void Start()
    {
        resistanceAppearance = ResourceManager.Materials.Resistances.Ice;
    }

    public override GameObject GetSource()
    {
        return ResourceManager.Sources.SwordEffects.Ice;
    }
}
