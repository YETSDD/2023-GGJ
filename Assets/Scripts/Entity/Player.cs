using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : EntityBase
{
    public bool Alive = true;
    public Player(GridBase grid) : base(grid)
    {
        Alive = true;
    }

    public bool TryMoveTo(GridBase grid)
    {
        //TODO
        grid.Trigger(this);
        bool canMove = true;
        if (canMove)
        {
            Body.MoveTo(grid);
            grid.Owner = this;
        }
        return canMove;
    }

    public bool TryMoveTo(Direction direct)
    {
        var vec = GridManager.Instance.DirectVector[direct];
        var currentGrid = HeadGrid;
        var currentX = currentGrid.PosX;
        var currentY = currentGrid.PosY;
        var targetGrid = GridManager.Instance.GetGrid(currentX + vec.x, currentY + vec.y);
        if (targetGrid == null)
        {
            Debug.LogWarning($"Invalid Index {currentX + vec.x}, {currentY + vec.y}");
            return false;
        }
        return TryMoveTo(targetGrid);
    }
}
