using UnityEngine;

public class FireResistance : ResistanceEffect
{
    public override string skillName => "Fire Resistance";

    private void Start()
    {
        resistanceAppearance = ResourceManager.Materials.Resistances.Fire;
    }

    public override GameObject GetSource()
    {
        return ResourceManager.Sources.SwordEffects.Fire;
    }
}
