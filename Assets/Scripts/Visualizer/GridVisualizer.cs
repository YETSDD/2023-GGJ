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
        Color.RGBToHSV(grid.BaseColor, out var h, out var s, out var v);


        //if (!grid.Movable)
        //{
        //    s = 0;
        //}
        if (grid.Owner != null)
        {
            h = 0;
            s = 0;
            v = 0.5f;
        }
        else
        {
            //h = 0.5f;
            //s = 1f;
            v = 1f;
        }

        if (!grid.Visible)
        {
            v = 0;
        }

        Color color = Color.HSVToRGB(h, s, v);
        spriteRenderer.color = color;
    }
}
