using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body
{
    public CustomNode<GridBase> Head;
    public Body(GridBase grid)
    {
        Head = new CustomNode<GridBase>(grid);
    }

    public void MoveTo(GridBase grid)
    {
        var newNode = new CustomNode<GridBase>(grid);
        Head.SetNext(newNode);
        Head = newNode;
    }

    public void Combine(Body other)
    {
        if (other == this)
        {
            //�Լ����Լ�
            return;
        }


    }
}
