using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager
{
    private static GridManager instance;
    public static GridManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GridManager();
            }
            return instance;
        }
    }
    const int size = 1;

    public int Height;
    public int Width;

    private List<List<GridBase>> Map;

    /// <summary>
    /// y
    /// 2
    /// 1
    /// 0 1 2 x
    /// </summary>
    public void Initialize(int x, int y)
    {
        Width = x;
        Height = y;
        Map = new List<List<GridBase>>();
        for (int i = 0; i < Height; i++)
        {
            var line = new List<GridBase>();
            for (int j = 0; j < Width; j++)
            {
                line.Add(new GridBase(j, i));
            }
            Map.Add(line);
        }
    }

    /// <summary>
    /// 距离中心点的坐标
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public Vector2 GetPos(GridBase grid)
    {
        var centerWorld = new Vector2(0, 0);
        var centerIndex = new Vector2Int(Width / 2, Height / 2);

        var gridIndex = new Vector2Int(grid.PosX, grid.PosY);
        var gridWorld = centerWorld + (gridIndex - centerIndex) * size;
        return gridWorld;
    }

    public GridBase GetGrid(int x, int y)
    {
        return Map.GetGrid(y, x);
    }

    public GridBase GetRightGrid(GridBase grid)
    {
        return GetGrid(grid.PosX + 1, grid.PosY);
    }
}
