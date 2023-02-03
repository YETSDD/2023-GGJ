using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapHelper
{
    public static GridBase GetGrid(this List<List<GridBase>> map, int x, int y)
    {
        if (map == null || map.Count == 0 || map[0].Count == 0)
        {
            Debug.LogError("Invalid Map");
            return null;
        }
        if (x < 0 || x > map[0].Count - 1 || y < 0 || y > map.Count - 1)
        {
            Debug.LogError("Invalid Index");
            return null;
        }
        return map[x][y];
    }
}
