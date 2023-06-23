using System;

public class BinarySearchTree<T> : Tree<T> where T : IComparable<T>
{
    public class Node : Tree<T>.Node
    {
        public T Value { get; set; }
        public new Node? Left { get; set; }
        public new Node? Right { get; set; }

        Tree<T>.Node? Tree<T>.Node.Left
        {
            get => Left;
        }

        Tree<T>.Node? Tree<T>.Node.Right
        {
            get => Right;
        }

        public Node(T value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    private Node? root;

    Tree<T>.Node? Tree<T>.Root => root;

    public Node? Root
    {
        get { return root; }
        private set { root = value; }
    }

    public void Insert(T value)
    {
        root = Insert(root, value);
    }

    private Node Insert(Node? node, T value)
    {
        if (node == null)
        {
            return new Node(value);
        }

        int comparison = value.CompareTo(node.Value);
        if (comparison < 0)
        {
            node.Left = Insert(node.Left, value);
        }
        else if (comparison > 0)
        {
            node.Right = Insert(node.Right, value);
        }

        return node;
    }

    public void Delete(T value)
    {
        root = Delete(root, value);
    }

    private Node? Delete(Node? node, T value)
    {
        if (node == null)
        {
            return node;
        }

        int comparison = value.CompareTo(node.Value);

        if (comparison < 0)
        {
            node.Left = Delete(node.Left, value);
        }
        else if (comparison > 0)
        {
            node.Right = Delete(node.Right, value);
        }
        else
        {
            if (node.Left == null)
            {
                return node.Right;
            }
            else if (node.Right == null)
            {
                return node.Left;
            }

            node.Value = MinValue(node.Right);

            node.Right = Delete(node.Right, node.Value);
        }

        return node;
    }

    private T MinValue(Node node)
    {
        T minValue = node.Value;
        while (node.Left != null)
        {
            minValue = node.Left.Value;
            node = node.Left;
        }
        return minValue;
    }
}