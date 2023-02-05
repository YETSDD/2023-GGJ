using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilGrid : GridBase
{
    public OilGrid(int x, int y) : base(x, y)
    {
        BaseColor = new Color(150, 181, 0);
    }

    public override void Trigger(EntityBase entity)
    {
        base.Trigger(entity);
        entity.Life += 5;
        //遇到火直接爆炸游戏结束
        entity.RemainChanceToDie--;
        if (entity.RemainChanceToDie <= 0)
        {
            entity.RemainChanceToDie = 1;
        }
    }
}
