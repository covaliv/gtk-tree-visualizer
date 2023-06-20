using System;

public class RedBlackTree<T> : Tree<T> where T : IComparable<T>
{
    public enum Color { Red, Black }

    public class Node : Tree<T>.Node
    {
        public T Value { get; set; }
        Tree<T>.Node? Tree<T>.Node.Left => Left;
        Tree<T>.Node? Tree<T>.Node.Right => Right;
        public Node? Left { get; set; }
        public Node? Right { get; set; }
        public Node? Parent { get; set; }
        public Color NodeColor { get; set; }

        public Node(T value)
        {
            Value = value;
            NodeColor = Color.Red;
        }

        public bool IsRed => NodeColor == Color.Red;
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

    public void Delete(T value)
{
    Node? nodeToDelete = FindNode(root, value);
    if (nodeToDelete == null) return;

    DeleteNode(nodeToDelete);
}

private Node? FindNode(Node? node, T value)
{
    if (node == null) return null;

    int comparisonResult = value.CompareTo(node.Value);
    if (comparisonResult < 0)
    {
        return FindNode(node.Left, value);
    }
    else if (comparisonResult > 0)
    {
        return FindNode(node.Right, value);
    }
    else
    {
        return node;
    }
}

private void DeleteNode(Node nodeToDelete)
{
    if (nodeToDelete.Left != null && nodeToDelete.Right != null)
    {
        Node replacement = GetMaxNode(nodeToDelete.Left);
        nodeToDelete.Value = replacement.Value;
        nodeToDelete = replacement;
    }

    Node? child = nodeToDelete.Left ?? nodeToDelete.Right;

    if (nodeToDelete.IsRed)
    {
        ReplaceNode(nodeToDelete, child);
    }
    else if (child != null && child.IsRed)
    {
        child.NodeColor = Color.Black;
        ReplaceNode(nodeToDelete, child);
    }
    else
    {
        Node? sibling = Sibling(nodeToDelete);
        if (sibling != null && sibling.IsRed)
        {
            nodeToDelete.Parent.NodeColor = Color.Red;
            sibling.NodeColor = Color.Black;

            if (nodeToDelete == nodeToDelete.Parent.Left)
            {
                RotateLeft(nodeToDelete.Parent);
            }
            else
            {
                RotateRight(nodeToDelete.Parent);
            }
        }

        sibling = Sibling(nodeToDelete);
        ReplaceNode(nodeToDelete, child);

        DeleteCase2(child);
    }
}

private Node GetMaxNode(Node node)
{
    while (node.Right != null)
    {
        node = node.Right;
    }

    return node;
}

private Node? Sibling(Node node)
{
    if (node.Parent == null) return null;

    if (node == node.Parent.Left)
    {
        return node.Parent.Right;
    }
    else
    {
        return node.Parent.Left;
    }
}

private void ReplaceNode(Node oldNode, Node? newNode)
{
    if (oldNode.Parent == null)
    {
        root = newNode;
    }
    else if (oldNode == oldNode.Parent.Left)
    {
        oldNode.Parent.Left = newNode;
    }
    else
    {
        oldNode.Parent.Right = newNode;
    }

    if (newNode != null)
    {
        newNode.Parent = oldNode.Parent;
    }
}

private void DeleteCase2(Node? node)
{
    if (node == null) return;

    Node? sibling = Sibling(node);
    if (sibling != null && !sibling.IsRed && (sibling.Left == null || !sibling.Left.IsRed) && (sibling.Right == null || !sibling.Right.IsRed))
    {
        sibling.NodeColor = Color.Red;
        if (node.Parent.IsRed)
        {
            node.Parent.NodeColor = Color.Black;
        }
        else
        {
            DeleteCase2(node.Parent);
        }
    }
    else
    {
        DeleteCase3(node);
    }
}

private void DeleteCase3(Node? node)
{
    if (node == null) return;

    Node? sibling = Sibling(node);
    if (sibling != null && !sibling.IsRed)
    {
        if (node == node.Parent.Left &&
            (sibling.Right == null || !sibling.Right.IsRed) &&
            sibling.Left != null && sibling.Left.IsRed)
        {
            sibling.NodeColor = Color.Red;
            sibling.Left.NodeColor = Color.Black;
            RotateRight(sibling);
        }
        else if (node == node.Parent.Right &&
                 (sibling.Left == null || !sibling.Left.IsRed) &&
                 sibling.Right != null && sibling.Right.IsRed)
        {
            sibling.NodeColor = Color.Red;
            sibling.Right.NodeColor = Color.Black;
            RotateLeft(sibling);
        }
    }

    DeleteCase4(node);
}

private void DeleteCase4(Node? node)
{
    if (node == null) return;

    Node? sibling = Sibling(node);
    if (sibling != null)
    {
        sibling.NodeColor = node.Parent.NodeColor;
        node.Parent.NodeColor = Color.Black;

        if (node == node.Parent.Left)
        {
            if (sibling.Right != null)
            {
                sibling.Right.NodeColor = Color.Black;
            }
            RotateLeft(node.Parent);
        }
        else
        {
            if (sibling.Left != null)
            {
                sibling.Left.NodeColor = Color.Black;
            }
            RotateRight(node.Parent);
        }
    }
}

}