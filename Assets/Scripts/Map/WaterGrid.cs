using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGrid : GridBase
{
    public WaterGrid(int x, int y) : base(x, y)
    {
        BaseColor = new Color(0, 95, 143);
    }

    public override void Trigger(EntityBase entity)
    {
        base.Trigger(entity);
        entity.Life += 2;
    }
}
