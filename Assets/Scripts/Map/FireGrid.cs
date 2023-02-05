using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGrid : GridBase
{
    public FireGrid(int x, int y) : base(x, y)
    {
        BaseColor = Color.red;

    }

    public override void Trigger(EntityBase entity)
    {
        base.Trigger(entity);
        //埋雷 再次踩到直接爆炸游戏结束
        entity.RemainChanceToDie--;
        if (entity.RemainChanceToDie <= 0)
        {
            entity.Life = 0;
        }
    }
}
