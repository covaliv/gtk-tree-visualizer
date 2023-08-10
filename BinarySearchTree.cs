using System.Text;

// This class represents a Binary Search Tree (BST), a tree data structure in which each node has at most two children, referred to as the left child and the right child.
public class BinarySearchTree<T> : Tree<T> where T : IComparable<T>
{
    // This property holds a log of updates made during BST operations.
    public StringBuilder updatesLog { get; private set; } = new StringBuilder();
    // This class represents a node in the BST.
    public class Node : Tree<T>.Node
    {
        // This property holds the value of the node.
        public T Value { get; set; }

        // These properties point to the left and right child nodes.
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        // These properties provide an interface for the base Node class's Left and Right properties.
        Tree<T>.Node? Tree<T>.Node.Left
        {
            get => Left;
        }

        Tree<T>.Node? Tree<T>.Node.Right
        {
            get => Right;
        }

        // This constructor initializes a new node with a given value and sets both child nodes to null.
        public Node(T value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    // This property points to the root node of the tree.
    private Node? root;

    // This property provides a getter for the root node, for use by the base Tree class.
    Tree<T>.Node? Tree<T>.Root => root;

    // This property provides a getter and private setter for the root node, for use within this class.
    public Node? Root
    {
        get { return root; }
        private set { root = value; }
    }

    // This method inserts a new value into the tree.
    public void Insert(T value)
    {
        updatesLog = new StringBuilder();
        root = Insert(root, value);
        updatesLog.AppendLine($"Inserted {value} into the tree.");
    }

    // This helper method recursively inserts a new value into the tree, using the BST property that all left descendants are less than the node and all right descendants are greater.
    private Node Insert(Node? node, T value)
    {
        if (node == null)
        {
            return new Node(value);
        }

        int comparison = value.CompareTo(node.Value);
        if (comparison < 0)
        {
            updatesLog.AppendLine($"Value {value} is less than {node.Value}, moving to the left child.");
            node.Left = Insert(node.Left, value);
        }
        else if (comparison > 0)
        {
            updatesLog.AppendLine($"Value {value} is greater than {node.Value}, moving to the right child.");
            node.Right = Insert(node.Right, value);
        }

        return node;
    }

    // This method deletes a value from the tree.
    public void Delete(T value)
    {
        updatesLog = new StringBuilder();
        root = Delete(root, value);
        updatesLog.AppendLine($"Deleted {value} from the tree.");
    }

    // This helper method recursively deletes a value from the tree, preserving the BST property.
    private Node? Delete(Node? node, T value)
    {
        if (node == null)
        {
            return node;
        }

        int comparison = value.CompareTo(node.Value);

        if (comparison < 0)
        {
            updatesLog.AppendLine($"Value {value} is less than {node.Value}, moving to the left child.");
            node.Left = Delete(node.Left, value);
        }
        else if (comparison > 0)
        {
            updatesLog.AppendLine($"Value {value} is greater than {node.Value}, moving to the right child.");
            node.Right = Delete(node.Right, value);
        }
        else
        {
            if (node.Left == null)
            {
                updatesLog.AppendLine($"Node {node.Value} is a leaf node or has only a right child. Replacing it with its right child.");
                return node.Right;
            }
            else if (node.Right == null)
            {
                updatesLog.AppendLine($"Node {node.Value} has only a left child. Replacing it with its left child.");
                return node.Left;
            }

            T minValue = MinValue(node.Right);
            updatesLog.AppendLine($"Node {node.Value} has two children. Replacing it with the smallest value {minValue} in its right subtree.");
            node.Value = minValue;

            node.Right = Delete(node.Right, node.Value);
        }

        return node;
    }

    // This helper method finds the minimum value in the subtree rooted at a given node.
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