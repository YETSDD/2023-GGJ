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
    Manure,
    Eye,
    End
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
    /// <summary>
    /// 可以来个字典 TODO
    /// </summary>
    public List<EndGrid> EndGrids;

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
        EndGrids = new List<EndGrid>();
        Map = new List<List<GridBase>>();
        for (int i = 0; i < Height; i++)
        {
            var line = new List<GridBase>();
            for (int j = 0; j < Width; j++)
            {
                var grid = RandomGrid(j, i);
                if (grid is EndGrid end)
                {
                    EndGrids.Add(end);
                }
                line.Add(grid);
            }
            Map.Add(line);
        }
    }

    private GridBase RandomGrid(int x, int y)
    {
        var posibility = GameManager.Instance.GenerateModels;
        int total = 0;
        posibility.ForEach(item => total += item.Weight);
        var random = Random.Range(0, total);
        GridType choose = GridType.Soil;
        int value = 0;
        for (int i = 0; i < posibility.Count; i++)
        {
            var model = posibility[i];
            if (value + model.Weight < random)
            {
                value += model.Weight;
            }
            else
            {
                choose = model.GridType;
                break;
            }
        }

        switch (choose)
        {
            case GridType.Soil: return new SoilGrid(x, y);
            case GridType.Water: return new WaterGrid(x, y);
            case GridType.Fire: return new FireGrid(x, y);
            case GridType.Rock: return new RockGrid(x, y);
            case GridType.Oil: return new OilGrid(x, y);
            case GridType.Manure: return new ManureGrid(x, y);
            case GridType.Eye: return new EyeGrid(x, y);
            case GridType.End: return new EndGrid(x, y);
        }
        return new SoilGrid(x, y);
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
[System.Serializable]
public class GenerateModel
{
    public GridType GridType;
    public int Weight;
}