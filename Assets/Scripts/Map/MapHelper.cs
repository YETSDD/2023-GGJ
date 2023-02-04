using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapHelper
{
    public static GridBase GetGrid(this List<List<GridBase>> map, int x, int y)
    {
        if (map == null || map.Count == 0 || map[0].Count == 0)
        {
            Debug.LogWarning("Invalid Map");
            return null;
        }
        if (x < 0 || x > map[0].Count - 1 || y < 0 || y > map.Count - 1)
        {
            Debug.LogWarning("Invalid Index");
            return null;
        }
        return map[x][y];
    }

    public static float GetDirection(this CustomNode<GridBase> node)
    {
        var grid = node.Data;
        if (grid == null || node.Next == null)
        {
            Debug.LogWarning("Empty");
            return -1;
        }
        var nextGrid = node.Next.Data;
        if (nextGrid == null)
        {
            Debug.LogWarning("Empty");
            return -1;
        }
        Vector2Int dis = new Vector2Int(nextGrid.PosX - grid.PosX, nextGrid.PosY - grid.PosY);
        return Vector2.SignedAngle(new Vector2Int(0, 1), dis);
    }
}
