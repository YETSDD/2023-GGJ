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

    public void Combine(Body other, GridBase grid)
    {
        if (other == this)
        {
            //自己吃自己
            Debug.LogWarning("EatSelf");
            return;
        }
        Queue<CustomNode<GridBase>> q = new Queue<CustomNode<GridBase>>();
        q.Enqueue(other.Head);
        while (q.Count > 0)
        {
            var node = q.Dequeue();
            foreach (var pre in node.Pres)
            {
                q.Enqueue(pre);
            }
            if (node.Data != null)
            {
                node.Data.Owner = Head.Data.Owner;
            }
        }
        var insertNode = other.Head.Find(grid);
        Head.SetNext(insertNode);
        Head = other.Head;

    }
}
