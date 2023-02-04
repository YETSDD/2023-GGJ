using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteExtension
{
    public static void ChangeV(this SpriteRenderer renderer, int value)
    {
        var color = renderer.color;
        Color.RGBToHSV(color, out var h, out var s, out var v);
        v += value;
        v = Mathf.Clamp(v, 0, 255);
        color = Color.HSVToRGB(h, s, v);
        renderer.color = color;
    }
}
