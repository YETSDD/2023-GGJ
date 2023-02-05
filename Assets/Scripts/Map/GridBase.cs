using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ݸ��������ĸ��ӵľ��������㼤��ȣ��Ӷ����Ƹ���Ƶ��
/// </summary>
public class GridBase
{
    public int AvailableTriggerTimes = 1;
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
        return Movable;
    }
    private bool canTrigger = true;
    public bool CanTrigger()
    {
        return canTrigger;
    }
    public virtual void Trigger(EntityBase entity)
    {
        AvailableTriggerTimes = AvailableTriggerTimes - 1 < 0 ? 0 : AvailableTriggerTimes - 1;
        Visible = true;
        if (entity == GameManager.Instance.Player)
        {
            Debug.Log($"Trigger {this.GetType().Name}");
        }
    }

    public virtual void MoveIn(EntityBase entity)
    {
        if (entity == GameManager.Instance.Player)
        {
            Debug.Log($"MoveIn {this.GetType().Name}");
        }
    }
}
