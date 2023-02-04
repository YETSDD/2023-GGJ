using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase
{
    public int Power;
    public int Life = 23;
    public GridBase HeadGrid => Body.Head.Data;
    public EntityBase(GridBase grid)
    {
        Body = new Body(grid);
        grid.Owner = this;
    }
    public Body Body;
}
