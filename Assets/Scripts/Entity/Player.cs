using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : EntityBase
{
    public bool Rooted = false;
    public bool Alive
    {
        get
        {
            return alive;
        }
        set
        {
            if (this == GameManager.Instance.Player)
            {
                Debug.Log("Die");
            }
            alive = value;
        }
    }
    public int LookCount = 1;
    private bool alive = true;

    public Player(GridBase grid) : base(grid)
    {
        Alive = true;
    }

    public bool TryMoveTo(GridBase grid)
    {
        Life--;
        if (Rooted) { return false; }
        if (grid.AvailableTriggerTimes > 0)
        {
            grid.Trigger(this);
        }
        bool canMove = grid.CanMove();
        if (canMove)
        {
            if (grid.Owner != null && grid.Owner != this)
            {
                //结合
                if (grid == grid.Owner.HeadGrid)
                {
                    if (grid.Owner is Player target && (this == GameManager.Instance.Player || target == GameManager.Instance.Player))
                    {
                        this.Rooted = true;
                        target.Rooted = true;
                        GameManager.Instance.NewGeneration(grid);

                        UIManager.Instance.ClearGrid();
                        //TODO 粒子特效
                    }
                }
                else
                {
                    //吞并
                    if (grid.Owner is Player target)
                    {
                        Debug.Log($"{target.HeadGrid.PosX},{target.HeadGrid.PosY} Is Eaten");

                        Body.Combine(target.Body, grid);

                        // 代价与收获
                        //Gain
                        LookCount += target.LookCount;

                        //Cost 生命共享
                        Life = (Life + target.Life + 1) / 2;

                        target.Alive = false;
                        target.Body = null;
                        target.Life = 0;
                        GameManager.Instance.AllPlayer.Remove(target);
                        UIManager.Instance.ClearGrid();
                    }

                }
            }
            else
            {
                grid.MoveIn(this);
                Body.MoveTo(grid);
                grid.Owner = this;
            }
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
