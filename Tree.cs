using System;

public interface Tree<T> where T : IComparable<T>
{
    void Insert(T value);
    void Delete(T value);

    public interface Node
    {
        T Value { get; }
        Node? Left { get; }
        Node? Right { get; }
    }

    Node? Root { get; }
}