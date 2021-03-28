
using UnityEngine;

public class ElementTypes
{
    public enum Type
    {
        Physical_Earth,
        Fire,
        Lightning,
        Cold_Ice,
        Energy
    }

    public static string Name(Type type)
    {
        switch(type)
        {
            case Type.Fire:
                return "Fire";
            case Type.Lightning:
                return "Lightning";
            case Type.Cold_Ice:
                return "Cold";
            case Type.Energy:
                return "Energy";
            default:
                return "Physical";
        }
    }

    public static Color Colors(Type type)
    {
        switch (type)
        {
            case Type.Fire:
                return new Color(255, 130, 0);
            case Type.Lightning:
                return new Color(0, 255, 160);
            case Type.Cold_Ice:
                return new Color(168, 246, 255);
            case Type.Energy:
                return new Color(255, 80, 255);
            default:
                return new Color(219, 192, 106);
        }
    }
}