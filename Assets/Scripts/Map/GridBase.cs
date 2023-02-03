using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 根据格子与中心格子的距离来计算激活度，从而控制更新频率
/// </summary>
public class GridBase
{
    public int PosX;
    public int PosY;
    public GridBase(int x, int y)
    {
        PosX = x;
        PosY = y;
    }


}
