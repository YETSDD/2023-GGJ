using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilGrid : GridBase
{
    public SoilGrid(int x, int y) : base(x, y)
    {
        BaseColor = new Color(122, 79, 22);
    }

    public override void Trigger(EntityBase entity)
    {
        base.Trigger(entity);
    }
}
