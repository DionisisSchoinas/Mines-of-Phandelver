using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalSlash : SimpleSlash
{
    public override float manaCost => 5f;

    public override Sprite GetIcon()
    {
        switch (attributes.elementType)
        {
            case ElementTypes.Type.Physical_Earth:
                return ResourceManager.UI.SkillIcons.Swing.Earth;
            case ElementTypes.Type.Cold_Ice:
                return ResourceManager.UI.SkillIcons.Swing.Ice;
            case ElementTypes.Type.Lightning:
                return ResourceManager.UI.SkillIcons.Swing.Lightning;
            default:
                return ResourceManager.UI.SkillIcons.Swing.Fire;
        }
    }
}
