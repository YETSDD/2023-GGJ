using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGrid : GridBase
{
    public WaterGrid(int x, int y) : base(x, y)
    {
    }

    public override void Trigger(EntityBase entity)
    {
        base.Trigger(entity);

    }
}
