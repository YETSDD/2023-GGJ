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

    //其实可能成环
    public CustomNode<T> Find(T data)
    {
        if (Data.Equals(data)) return this;
        foreach (var pre in Pres)
        {
            var result = pre.Find(data);
            if(result !=null) return result;
        }
        return null;
    }
}