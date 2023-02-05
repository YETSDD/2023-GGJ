using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController
{
    public Player Player;
    public AgentController(GridBase grid)
    {
        Player = new Player(grid);
    }

    public void Act()
    {
        var canMoveDirection = new List<Direction>();
        foreach (var pair in GridManager.Instance.DirectVector)
        {
            var grid = GridManager.Instance.GetGrid(Player.HeadGrid.PosX + pair.Value.x, Player.HeadGrid.PosY + pair.Value.y);
            if (grid == null)
            {
                continue;
            }
            else
            {
                //���ܳ����,���ܳ��Լ�
                if (grid.CanTrigger() && grid.Owner != Player && grid.Owner != GameManager.Instance.Player)
                {
                    canMoveDirection.Add(pair.Key);
                    continue;
                }
            }
        }

        var random = Random.Range(0, canMoveDirection.Count);
        if (canMoveDirection.Count == 0)
        {
            Debug.Log($"{Player.HeadGrid.PosX},{Player.HeadGrid.PosY} Stuck");
            Player.Alive = false;
            return;
        }
        var direct = canMoveDirection[random];
        var moveSuccess = Player.TryMoveTo(direct);
    }
}