using UnityEngine;

public class LightningResistance : ResistanceEffect
{
    public override string skillName => "Lightning Resistance";

    private void Start()
    {
        resistanceAppearance = ResourceManager.Materials.Resistances.Lightning;
    }
    public override GameObject GetSource()
    {
        return ResourceManager.Sources.SwordEffects.Lightning;
    }
}
