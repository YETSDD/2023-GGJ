using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GridVisualizer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();
    private Color baseColor;
    private void Start()
    {
        baseColor = spriteRenderer.color;
    }

    public void Refresh()
    {
        spriteRenderer.color = baseColor;
    }

    public void Show(GridBase grid)
    {

        Color.RGBToHSV(spriteRenderer.color, out var h, out var s, out var v);
        //if (!grid.Visible)
        //{
        //    v = 0;
        //}

        //if (!grid.Movable)
        //{
        //    s = 0;
        //}
        if (grid.Owner != null)
        {
            h = 255;
            s = 255;
            v = 255;
        }
        else
        {
            h = 123;
            s = 255;
            v = 255;
        }

        Color color = Color.HSVToRGB(h, s, v);

        spriteRenderer.color = color;
    }
}
