using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ݸ��������ĸ��ӵľ��������㼤��ȣ��Ӷ����Ƹ���Ƶ��
/// </summary>
public class GridBase
{
    public EntityBase Owner = null;
    public int PosX;
    public int PosY;
    public GridBase(int x, int y)
    {
        PosX = x;
        PosY = y;
    }

    public bool Visible = false;

    public bool Movable = true;

    public virtual void Trigger()
    {

    }
}
