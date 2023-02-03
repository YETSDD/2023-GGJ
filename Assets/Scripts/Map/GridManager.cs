using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int Height;
    public int Width;

    public List<List<GridBase>> Map;

    /// <summary>
    /// 0 1 2 x
    /// 1
    /// 2
    /// y
    /// </summary>
    public void Initialize()
    {
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
