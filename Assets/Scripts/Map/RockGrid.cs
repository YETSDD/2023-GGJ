using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGrid : GridBase
{
    int count = 1;
    public RockGrid(int x, int y) : base(x, y)
    {
    }

    public override void Trigger(EntityBase entity)
    {
        base.Trigger(entity);
        count--;
        Color.RGBToHSV(BaseColor, out var h, out var s, out var v);
        v += 0.1f;
        BaseColor = Color.HSVToRGB(h, s, v);

        if (count <= 0)
        {
            Movable = true;
        }
    }
}
