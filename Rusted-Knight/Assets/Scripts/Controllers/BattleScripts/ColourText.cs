using UnityEngine;
using TMPro;

public static class ColourText : object
{




    public static string RedString(string s)
    {
        return $"<color=#FF0000>{s}</color>";
    }

    public static string BlueString(string s)
    {
        return $"<color=#0000FF>{s}</color>";
    }

    public static string GreenString(string s)
    {
        return $"<color=#00FF00>{s}</color>";
    }

    public static string YellowString(string s)
    {
        return $"<color=#FFFF00>{s}</color>";
    }
}