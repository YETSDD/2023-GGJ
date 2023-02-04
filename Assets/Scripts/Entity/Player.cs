using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : EntityBase
{
    public int Power;
    public int Life = 23;

    public Player(GridBase grid) : base(grid)
    {
    }

    public bool TryMoveTo(GridBase grid)
    {
        //TODO
        grid.Trigger();
        bool canMove = true;
        if (canMove)
        {
            Body.MoveTo(grid);
            grid.Owner = this;
        }
        return canMove;
    }

}
