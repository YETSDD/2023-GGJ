using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridType
{
    Soil,
    Oil,
    Rock,
    Fire,
    Water,
    /// <summary>
    /// 答辩
    /// </summary>
    Manure
}
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
    public readonly Dictionary<Direction, Vector2Int> DirectVector = new Dictionary<Direction, Vector2Int>() { { Direction.Left, new Vector2Int(-1, 0) }, { Direction.Right, new Vector2Int(1, 0) }, { Direction.Up, new Vector2Int(0, 1) }, { Direction.Down, new Vector2Int(0, -1) } };

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
        //TODO 随机生成算法
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

    private GridBase RandomGrid(int x, int y)
    {
        var random = Random.Range(0, 6);
        switch (random)
        {

        }
        return null;
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

    /// <summary>
    /// 来个标记数组 TODO？
    /// </summary>
    /// <returns></returns>
    public GridBase GetRandomGeneratePoint()
    {
        GridBase result = null;
        while (result == null || result.Owner != null)
        {
            int randomX = Random.Range(0, Width);
            int randomY = Random.Range(0, Height);
            result = GetGrid(randomX, randomY);
        }
        return result;
    }
}
