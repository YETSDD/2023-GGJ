using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase
{
    public int Life
    {
        get
        {
            return life;
        }
        set
        {
            if (value < 0)
            {
                life = 0;
                return;
            }
            life = value;
        }
    }
    private int life = 23;

    public int RemainChanceToDie = 2;
    public GridBase HeadGrid => Body?.Head?.Data;
    public EntityBase(GridBase grid)
    {
        Body = new Body(grid);
        grid.Owner = this;
    }
    public Body Body;
}
