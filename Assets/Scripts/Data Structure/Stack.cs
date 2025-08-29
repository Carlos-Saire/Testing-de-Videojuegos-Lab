using System;
public class Stack<T>
{
    private class Node
    {
        public Node Next { get; set; }
        public Node Previos { get; set; }
        public T Value { get; set; }
        public Node(T value)
        {
            Value = value;
            Next = null;
            Previos = null;
        }
    }
    private Node Head;
    private Node Top;
    private int count;
    public int Count => count;
    public void Push(T value)
    {
        Node NewNode = new Node(value);
        if (Head == null)
        {
            NewNode.Previos = null;
            Head = NewNode;
            Top = NewNode;
        }
        else
        {
            Top.Next = NewNode;
            NewNode.Previos = Top;
            Top = NewNode;
        }
        ++count;

    }
    public T Pop()
    {
        if (count > 0)
        {
            T value = Top.Value;
            Top = Top.Previos;
            --count;
            if (count == 0)
            {
                Head = null; 
                Top = null;
            }
            return value;
        }
        else
        {
            throw new NullReferenceException("Lista Vacia");
        }
    }
}
