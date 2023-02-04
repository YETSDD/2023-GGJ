using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinableTree<T>
{
    public CustomNode<T> RootNode { get; private set; }
}

public class CustomNode<T>
{
    public T Data;
    public CustomNode<T> Next { get; private set; }
    public List<CustomNode<T>> Pres { get; private set; }
    public CustomNode(T data)
    {
        Data = data;
        Pres = new List<CustomNode<T>>();
    }

    public void SetNext(CustomNode<T> next)
    {
        next.Pres.Add(this);
        Next = next;
    }
}