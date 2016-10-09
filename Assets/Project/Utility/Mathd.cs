using UnityEngine;
using System.Collections;

/// <summary>
/// Double precision variant of Mathf
/// </summary>
public static class Mathd {

    public static float InverseLerp(double a, double b, double value)
    {
        return (float)((value - a) / (b - a));
    }
}
