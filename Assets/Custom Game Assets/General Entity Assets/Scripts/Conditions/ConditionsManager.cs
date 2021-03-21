using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionsManager
{
    public static Condition Burning = new Condition();
    public static Condition Electrified = new Condition().Name("Electrified").DamageType(ElementTypes.Type.Lightning).Effect(ResourceManager.Effects.Electrified);
    public static Condition Frozen = new Condition().Name("Frozen").DamageType(ElementTypes.Type.Cold_Ice).Effect(ResourceManager.Effects.Frozen);
}
