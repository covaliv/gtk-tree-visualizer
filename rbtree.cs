using System;
using System.Collections.Generic;

public class RedBlackTree<T> where T : IComparable<T>
{
    public enum Color { Red, Black }

    public class Node
    {
        public T Value;
        public Node? Left;
        public Node? Right;
        public Node? Parent;
        public Color NodeColor;

        public Node(T value)
        {
            Value = value;
            NodeColor = Color.Red;
        }

        public bool IsRed => NodeColor == Color.Red;
    }

    private Node? root;

    public Node Root
    {
        get
        {
            if (root == null)
            {
                throw new NullReferenceException("The root is null.");
            }
            return root;
        }
    }

    public void Insert(T value)
    {
        if (root == null)
        {
            root = new Node(value) { NodeColor = Color.Black };
            Console.WriteLine($"Inserted {value} as root.");
        }
        else
        {
            Node currentNode = root;
            Node newNode = new Node(value);

            while (true)
            {
                int comparison = value.CompareTo(currentNode.Value);
                if (comparison < 0)
                {
                    if (currentNode.Left == null)
                    {
                        currentNode.Left = newNode;
                        newNode.Parent = currentNode;
                        Console.WriteLine($"Inserted {value} as left child of {currentNode.Value}.");
                        break;
                    }
                    currentNode = currentNode.Left;
                }
                else if (comparison > 0)
                {
                    if (currentNode.Right == null)
                    {
                        currentNode.Right = newNode;
                        newNode.Parent = currentNode;
                        Console.WriteLine($"Inserted {value} as right child of {currentNode.Value}.");
                        break;
                    }
                    currentNode = currentNode.Right;
                }
                else
                {
                    Console.WriteLine($"Value {value} already exists in the tree.");
                    return;
                }
            }

            FixTreeAfterInsert(newNode);
        }
    }

    private void FixTreeAfterInsert(Node node)
    {
        while (node != root && node.Parent.NodeColor == Color.Red)
        {
            if (node.Parent == node.Parent.Parent.Left)
            {
                Node uncle = node.Parent.Parent.Right;
                if (uncle != null && uncle.NodeColor == Color.Red)
                {
                    node.Parent.NodeColor = Color.Black;
                    uncle.NodeColor = Color.Black;
                    node.Parent.Parent.NodeColor = Color.Red;
                    node = node.Parent.Parent;
                }
                else
                {
                    if (node == node.Parent.Right)
                    {
                        node = node.Parent;
                        RotateLeft(node);
                    }
                    node.Parent.NodeColor = Color.Black;
                    node.Parent.Parent.NodeColor = Color.Red;
                    RotateRight(node.Parent.Parent);
                }
            }
            else
            {
                Node uncle = node.Parent.Parent.Left;
                if (uncle != null && uncle.NodeColor == Color.Red)
                {
                    node.Parent.NodeColor = Color.Black;
                    uncle.NodeColor = Color.Black;
                    node.Parent.Parent.NodeColor = Color.Red;
                    node = node.Parent.Parent;
                }
                else
                {
                    if (node == node.Parent.Left)
                    {
                        node = node.Parent;
                        RotateRight(node);
                    }
                    node.Parent.NodeColor = Color.Black;
                    node.Parent.Parent.NodeColor = Color.Red;
                    RotateLeft(node.Parent.Parent);
                }
            }
        }

        root.NodeColor = Color.Black;
    }

    private void RotateLeft(Node node)
    {
        Node rightChild = node.Right;
        node.Right = rightChild.Left;

        if (rightChild.Left != null)
        {
            rightChild.Left.Parent = node;
        }

        rightChild.Parent = node.Parent;

        if (node.Parent == null)
        {
            root = rightChild;
        }
        else if (node == node.Parent.Left)
        {
            node.Parent.Left = rightChild;
        }
        else
        {
            node.Parent.Right = rightChild;
        }

        rightChild.Left = node;
        node.Parent = rightChild;

        Console.WriteLine($"Rotated left around {node.Value}.");
    }

    private void RotateRight(Node node)
    {
        Node leftChild = node.Left;
        node.Left = leftChild.Right;

        if (leftChild.Right != null)
        {
            leftChild.Right.Parent = node;
        }

        leftChild.Parent = node.Parent;

        if (node.Parent == null)
        {
            root = leftChild;
        }
        else if (node == node.Parent.Right)
        {
            node.Parent.Right = leftChild;
        }
        else
        {
            node.Parent.Left = leftChild;
        }

        leftChild.Right = node;
        node.Parent = leftChild;

        Console.WriteLine($"Rotated right around {node.Value}.");
    }

    public int Depth()
{
    return Depth(Root);
}

private int Depth(Node node)
{
    if (node == null)
    {
        return 0;
    }

    int leftDepth = Depth(node.Left);
    int rightDepth = Depth(node.Right);

    return Math.Max(leftDepth, rightDepth) + 1;
}

    public void PrintTree()
{
    if (root == null)
    {
        Console.WriteLine("The tree is empty.");
    }
    else
    {
        Console.WriteLine("Red-Black Tree:");
        PrintNode(root, string.Empty);
    }

    
}

private void PrintNode(Node? node, string indent)
{
    if (node != null)
    {
        string color = node.NodeColor == Color.Red ? "R" : "B";
        string leftChild = node.Left != null ? node.Left.Value.ToString() : "null";
        string rightChild = node.Right != null ? node.Right.Value.ToString() : "null";
        string parent = node.Parent != null ? node.Parent.Value.ToString() : "null";
        Console.WriteLine($"{indent}V: {node.Value} (C: {color}, L: {leftChild}, R: {rightChild}, P: {parent})");

        string childIndent = indent + "  ";

        PrintNode(node.Left, childIndent);
        PrintNode(node.Right, childIndent);
    }
}


}