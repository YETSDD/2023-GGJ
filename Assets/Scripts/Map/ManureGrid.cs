using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManureGrid : GridBase
{
    public ManureGrid(int x, int y) : base(x, y)
    {
        BaseColor = new Color(237, 153, 43);

    }

    public override void Trigger(EntityBase entity)
    {
        base.Trigger(entity);
        entity.Life += 5;
        entity.RemainChanceToDie++;
        //TODO 展示表情
    }
}
