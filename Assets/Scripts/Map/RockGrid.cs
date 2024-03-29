using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGrid : GridBase
{
    int count = 2;
    public RockGrid(int x, int y) : base(x, y)
    {
        BaseColor = Color.gray;
        AvailableTriggerTimes = 3;
        Movable = false;
    }

    public override void Trigger(EntityBase entity)
    {
        base.Trigger(entity);
        count--;
        Color.RGBToHSV(BaseColor, out var h, out var s, out var v);
        v -= 0.2f;
        v = Mathf.Clamp01(v);
        BaseColor = Color.HSVToRGB(h, s, v);

        if (count <= 0)
        {
            Movable = true;
        }
        else
        {
            Movable = false;
        }
    }
}
