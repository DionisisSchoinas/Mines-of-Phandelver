
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

    public static string Condition(Type type)
    {
        switch (type)
        {
            case Type.Fire:
                return "Burning";
            case Type.Lightning:
                return "Electrified";
            case Type.Cold_Ice:
                return "Freezing";
            case Type.Energy:
                return "No";
            default:
                return "No";
        }
    }

    public static Color Colors(Type type)
    {
        switch (type)
        {
            case Type.Fire:
                return new Color(1f, 0.532f, 0f);
            case Type.Lightning:
                return new Color(0f, 1f, 0.6274f);
            case Type.Cold_Ice:
                return new Color(0.5707f, 0.9407f, 255f);
            case Type.Energy:
                return new Color(0.85f, 0.45f, 255f);
            default:
                return new Color(0.8f, 0.6f, 0.23f);
        }
    }
}