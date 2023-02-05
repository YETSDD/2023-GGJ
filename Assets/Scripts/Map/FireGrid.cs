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
        //���� �ٴβȵ�ֱ�ӱ�ը��Ϸ����
        entity.RemainChanceToDie--;
        if (entity.RemainChanceToDie <= 0)
        {
            entity.Life = 0;
        }
    }
}
