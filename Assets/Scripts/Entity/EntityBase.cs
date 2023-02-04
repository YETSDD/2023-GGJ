using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase
{
    public GridBase HeadGrid => Body.Head.Data;
    public EntityBase(GridBase grid)
    {
        Body = new Body(grid);
    }
    public Body Body;
}
