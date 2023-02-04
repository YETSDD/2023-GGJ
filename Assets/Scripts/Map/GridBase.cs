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
    public Color BaseColor;
    public GridBase(int x, int y)
    {
        PosX = x;
        PosY = y;
    }

    public bool Visible = false;

    public bool Movable = true;
    public bool CanMove()
    {
        return Movable && Owner == null;
    }
    public virtual void Trigger(EntityBase entity)
    {
        Visible = true;
    }
}
