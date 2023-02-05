using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeGrid : GridBase
{
    int radius = 2;
    public EyeGrid(int x, int y) : base(x, y)
    {
        BaseColor = new Color(200, 200, 22);

    }

    public override void Trigger(EntityBase entity)
    {
        base.Trigger(entity);
        if (entity is Player player)
        {
            player.LookCount = 1;
        }
        //展示周围25格
        for (int i = -radius; i <= radius; i++)
        {
            for (int j = -radius; j <= radius; j++)
            {
                var grid = GridManager.Instance.GetGrid(PosX + i, PosY + j);
                if (grid != null)
                {
                    grid.Visible = true;
                }
            }
        }
    }
}
