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
    public CustomNode(T data)
    {
        Data = data;
    }

    public void SetNext(CustomNode<T> next)
    {
        Next = next;
    }
}