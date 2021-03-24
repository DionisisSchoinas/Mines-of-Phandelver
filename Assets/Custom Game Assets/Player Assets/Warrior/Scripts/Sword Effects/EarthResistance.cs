using UnityEngine;

public class EarthResistance : ResistanceEffect
{
    public override string skillName => "Physical Resistance";

    private void Start()
    {
        resistanceAppearance = ResourceManager.Materials.Resistances.Physical;
    }

    public override GameObject GetSource()
    {
        return ResourceManager.Sources.SwordEffects.Earth;
    }
}
