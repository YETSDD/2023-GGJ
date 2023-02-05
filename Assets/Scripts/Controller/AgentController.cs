using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AgentController
{
    public Player Player;
    private int targetPosX;
    private int targetPosY;
    public AgentController(GridBase grid)
    {
        Player = new Player(grid);
        targetPosX = grid.PosX;
        targetPosY = grid.PosY;
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
                //不能吃玩家,不能吃自己
                if (grid.CanTrigger() && grid.Owner != Player && grid.Owner != GameManager.Instance.Player)
                {
                    canMoveDirection.Add(pair.Key);
                    continue;
                }
            }
        }

        var direct = FindEndGrid(canMoveDirection);
        if (direct != Direction.None)
        {
            var moveSuccess = Player.TryMoveTo(direct);
        }

    }

    private Direction RandomChoose(List<Direction> canMoveDirection)
    {
        var random = Random.Range(0, canMoveDirection.Count);
        if (canMoveDirection.Count == 0)
        {
            Debug.Log($"{Player.HeadGrid.PosX},{Player.HeadGrid.PosY} Stuck");
            Player.Alive = false;
            return Direction.None;
        }

        var direct = canMoveDirection[random];
        return direct;
    }

    /// <summary>
    /// 伪B*
    /// </summary>
    /// <param name="canMoveDirection"></param>
    /// <returns></returns>
    public Direction FindEndGrid(List<Direction> canMoveDirection)
    {
        int currentX = Player.HeadGrid.PosX;
        int currentY = Player.HeadGrid.PosY;
        if (targetPosX == currentX && targetPosY == currentY)
        {
            GetTarget();
        }
        var sourceDis = GetSqrDistance(targetPosX, targetPosY, currentX, currentY);

        Dictionary<Direction, float> scores = new Dictionary<Direction, float>();
        foreach (var direction in canMoveDirection)
        {
            var directVec = GridManager.Instance.DirectVector[direction];
            int posXAfterMove = currentX + directVec.x;
            int posYAfterMove = currentY + directVec.y;
            var disAfterMove = GetSqrDistance(targetPosX, targetPosY, posXAfterMove, posYAfterMove);
            var disScore = Mathf.Sqrt(sourceDis) - Mathf.Sqrt(disAfterMove);

            scores.Add(direction, disScore);
        }

        //加入一点随机噪点
        var keys = scores.Keys.ToList();
        foreach (var key in keys)
        {
            scores[key] += Random.Range(-0.5f, 0.5f);
        }

        var chosenDirect = Direction.None;
        float maxScore = float.MinValue;
        foreach (var pair in scores)
        {
            if (pair.Value > maxScore)
            {
                maxScore = pair.Value;
                chosenDirect = pair.Key;
            }

        }

        return chosenDirect;
    }

    /// <summary>
    /// TODO 对结果进行处理
    /// </summary>
    private void GetTarget()
    {
        int currentX = Player.HeadGrid.PosX;
        int currentY = Player.HeadGrid.PosY;

        int resultX = -1;
        int resultY = -1;
        var targets = GridManager.Instance.EndGrids;
        float min = float.MaxValue;
        foreach (var end in targets)
        {
            var endX = end.PosX;
            var endY = end.PosY;
            int dis = GetSqrDistance(currentX, currentY, endX, endY);
            if (dis < min)
            {
                min = dis;
                resultX = end.PosX;
                resultY = end.PosY;
            }
        }

        targetPosX = resultX;
        targetPosY = resultY;
    }

    private static int GetSqrDistance(int currentX, int currentY, int endX, int endY)
    {
        var disX = currentX - endX;
        var disY = currentY - endY;
        var dis = disX * disX + disY * disY;
        return dis;
    }
}
