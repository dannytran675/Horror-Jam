using UnityEngine;
using TMPro;

public static class ColourText : object
{




    public static string RedString(string s)
    {
        return $"<color=#FF0000>{s}</color>";
    }

    public static string DarkRedString(string s)
    {
        return $"<color=#6B0000>{s}</color>";
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

    public static string GrayString(string s)
    {
        return $"<color=#141414>{s}</color>";
    }

    public static string KnightColourString(string s)
    {
        return $"<color=#607C3C>{s}</color>";
    }

    public static string DenaColourString(string s)
    {
        return $"<color=#FBB954>{s}</color>";
    }
    public static string FlontColourString(string s)
    {
        return $"<color=#FFC5C3>{s}</color>";
    }

    public static string BossColourString(string s)
    {
        return $"<color=#603B61>{s}</color>";
    }
}